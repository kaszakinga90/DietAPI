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
                        var nutrient = _mapper.Map<IngredientNutrient>(nutrientDTO);
                        nutrient.IngredientId = ingredient.Id;
                        _context.IngredientNutrientsDb.Add(nutrient);
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
