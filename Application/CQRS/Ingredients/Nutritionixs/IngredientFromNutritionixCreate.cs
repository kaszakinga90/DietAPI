using Application.Core;
using Application.DTOs.IngredientDTO.IngredientNutritionixDTO;
using Application.DTOs.LogoDTO;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.CQRS.Ingredients.Nutritionixs
{
    public class IngredientFromNutritionixCreate
    {
        public class Command : IRequest<Result<IngredientNutritionixDTO>>
        {
            public IngredientNutritionixDTO IngredientNutritionixDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<IngredientNutritionixDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<IngredientNutritionixDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                
                var ingredient = _mapper.Map<Ingredient>(request.IngredientNutritionixDTO);
                
                _context.IngredientsDb.Add(ingredient);



                await _context.SaveChangesAsync();



                if (request.IngredientNutritionixDTO.NutrientsDTO != null && request.IngredientNutritionixDTO.NutrientsDTO.Any())
                {
                    foreach (var nutrientDTO in request.IngredientNutritionixDTO.NutrientsDTO)
                    {
                        var existingNutrient = await _context.IngredientNutrientsDb
                            .FindAsync(ingredient.Id, nutrientDTO.NutrientId);

                        if (existingNutrient != null)
                        {
                            _context.Entry(existingNutrient).CurrentValues.SetValues(nutrientDTO);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newNutrient = _mapper.Map<IngredientNutrient>(nutrientDTO);
                            newNutrient.IngredientId = ingredient.Id;
                            _context.IngredientNutrientsDb.Add(newNutrient);
                            await _context.SaveChangesAsync();
                        }
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                }

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<IngredientNutritionixDTO>.Failure("Dodanie składnika nie powiodło się.");
                    }
                }
                catch (Exception ex)
                {
                    return Result<IngredientNutritionixDTO>.Failure("Wystąpił błąd podczas dodawania składnika: " + ex.Message);
                }

                return Result<IngredientNutritionixDTO>.Success(_mapper.Map<IngredientNutritionixDTO>(ingredient));

            }

        }
    }
}

// TODO : działa dobrze, ale wyświetla zły komunikat