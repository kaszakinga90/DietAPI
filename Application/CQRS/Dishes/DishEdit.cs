using Application.Core;
using Application.DTOs.DishDTO;
using Application.Validators.Dish;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Dishes
{
    public class DishEdit
    {
        public class Command : IRequest<Result<DishEditDTO>>
        {
            public DishEditDTO DishEditDTO { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<DishEditDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly DishUpdateValidator _validator;

            public Handler(DietContext context, IMapper mapper, DishUpdateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<DishEditDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.DishEditDTO, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<DishEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var requestDish = request.DishEditDTO;

                try
                {

                    var dish = await _context.DishesDb.FindAsync(requestDish.Id);

                    if (dish == null)
                    {
                        return Result<DishEditDTO>.Failure("Dish not found.");
                    }

                    var relations = _context.MealTimesDb.Any(mt => mt.DishId == dish.Id);

                    if (relations)
                    {
                        return Result<DishEditDTO>.Failure("Dish is being used in another tabel. Cannot edit.");
                    }

                    dish.Name = requestDish.Name;
                    dish.Calories = requestDish.Calories;
                    dish.ServingQuantity = requestDish.ServingQuantity;
                    dish.MeasureId = requestDish.MeasureId;
                    dish.UnitId = requestDish.UnitId;
                    dish.GlycemicIndex = requestDish.GlycemicIndex;
                    dish.PreparingTime = requestDish.PreparingTime;
                    dish.DieticianId = requestDish.DieteticianId;

                    dish.DishFoodCatalogs = requestDish.DishFoodCatalogs?.Select(dto => _mapper.Map<DishFoodCatalog>(dto)).ToList();
                    dish.DishIngredients = requestDish.DishIngredients?.Select(dto => _mapper.Map<DishIngredient>(dto)).ToList();

                    var existingRecipe = await _context.RecipesDb.FindAsync(dish.RecipeId);
                    existingRecipe.Steps = _mapper.Map<List<RecipeStep>>(requestDish.RecipeSteps);

                    await _context.SaveChangesAsync();

                    return Result<DishEditDTO>.Success(_mapper.Map<DishEditDTO>(dish));

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DishEditDTO>.Failure("Wystąpił błąd podczas przetwarzania danych.");
                }
            }
        }
    }
}