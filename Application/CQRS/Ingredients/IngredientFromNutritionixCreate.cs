using Application.DTOs.IngredientDTO;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.CQRS.Ingredients
{
    public class IngredientFromNutritionixCreate
    {
        public class Command : IRequest
        {
            public IngredientDTO IngredientDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var ingredient = _mapper.Map<Ingredient>(request.IngredientDTO);

                _context.IngredientsDb.Add(ingredient);
                await _context.SaveChangesAsync();

                if (request.IngredientDTO.NutrientsDTO != null && request.IngredientDTO.NutrientsDTO.Any())
                {
                    foreach (var nutrientDTO in request.IngredientDTO.NutrientsDTO)
                    {
                        var existingNutrient = await _context.IngredientNutrientsDb
                            .FindAsync(ingredient.Id, nutrientDTO.NutrientId);

                        if (existingNutrient != null)
                        {
                            // Aktualizuj istniejącą encję
                            _context.Entry(existingNutrient).CurrentValues.SetValues(nutrientDTO);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // Dodaj nową encję
                            var newNutrient = _mapper.Map<IngredientNutrient>(nutrientDTO);
                            newNutrient.IngredientId = ingredient.Id;
                            _context.IngredientNutrientsDb.Add(newNutrient);
                            await _context.SaveChangesAsync();
                        }
                    }

                    await _context.SaveChangesAsync();
                }
            }

        }
    }
}
