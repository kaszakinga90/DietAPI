using Application.DTOs.DietDTO;
using MediatR;
using DietDB;
using Application.Core;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.DTOs.RecipeDTO;
using Application.DTOs.DishDTO;
using Application.DTOs.DishIngredientDTO;

namespace Application.CQRS.Diets
{
    public class DietDetails
    {
        public class Query : IRequest<Result<DietDetailsGetDTO>>
        {
            public int DietId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DietDetailsGetDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<DietDetailsGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var diet = await _context.DietsDb
                            .Where(d => d.Id == request.DietId)
                             .Include(d => d.Patient)
                             .Include(d => d.Dietician)
                             .Include(d => d.MealTimesToXYAxis)
                                 .ThenInclude(mt => mt.Meal)
                             .Include(d => d.MealTimesToXYAxis)
                                 .ThenInclude(mt => mt.Dish)
                                     .ThenInclude(dish => dish.Measure)
                            .Include(d => d.MealTimesToXYAxis)
                                 .ThenInclude(mt => mt.Dish)
                                     .ThenInclude(dish => dish.Unit)
                            .Include(d => d.MealTimesToXYAxis)
                                 .ThenInclude(mt => mt.Dish)
                                     .ThenInclude(dish => dish.DishIngredients)
                                         .ThenInclude(di => di.Ingredient)
                                            .ThenInclude(i => i.Unit)
                            .Include(d => d.MealTimesToXYAxis)
                                 .ThenInclude(mt => mt.Dish)
                                     .ThenInclude(dish => dish.Recipe)
                                     .ThenInclude(recipe => recipe.Steps)
                             .FirstOrDefaultAsync(d => d.Id == request.DietId);

                    if (diet == null)
                    {
                        return Result<DietDetailsGetDTO>.Failure("Nie znaleziono diety");
                    }

                    var dietDetailsDto = new DietDetailsGetDTO
                    {
                        Id = diet.Id,
                        Name = diet.Name,
                        StartDate = diet.StartDate.Date.ToShortDateString(),
                        EndDate = diet.EndDate.Date.ToShortDateString(),
                        PatientName = diet.Patient?.Email,
                        DieticianName = diet.Dietician?.Email,
                        numberOfMeals = diet.numberOfMeals,
                        MealTimeToDietDetailsGetDTO = new List<MealTimeToDietDetailsGetDTO>(),
                    };

                    foreach (var mealTimeToXYAxis in diet.MealTimesToXYAxis)
                    {
                        var mealTimeToDietDetailsDto = new MealTimeToDietDetailsGetDTO
                        {
                            MealName = mealTimeToXYAxis.Meal.Name,
                            MealDate = mealTimeToXYAxis.MealTime.Date.ToShortDateString(),
                            MealTime = mealTimeToXYAxis.MealTime.ToShortTimeString(),
                            DishToDietDetailsGetDTO = new DishToDietDetailsGetDTO
                            {
                                Name = mealTimeToXYAxis.Dish?.Name,
                                Calories = mealTimeToXYAxis.Dish.Calories,
                                ServingQuantity = mealTimeToXYAxis.Dish.ServingQuantity,
                                GlycemicIndex = mealTimeToXYAxis.Dish.GlycemicIndex,
                                PreparingTime = mealTimeToXYAxis.Dish.PreparingTime,
                                MeasureName = mealTimeToXYAxis.Dish.Measure.Symbol,
                                UnitName = mealTimeToXYAxis.Dish.Unit.Symbol,
                                DishIngredients = mealTimeToXYAxis.Dish?.DishIngredients?.Select(di => new DishIngredientToDietDetailsGetDTO
                                {
                                    IngredientName = di.Ingredient?.Name,
                                    Quantity = di.Quantity,
                                    UnitName = di.Unit.Symbol,
                                }).ToList(),

                                RecipeStepsDTO = mealTimeToXYAxis.Dish?.Recipe?.Steps?.Select(step => new RecipeStepToDietDetailsGetDTO
                                {
                                    StepNumber = step.StepNumber,
                                    Description = step.Description
                                }).ToList()
                            }
                        };

                        dietDetailsDto.MealTimeToDietDetailsGetDTO.Add(mealTimeToDietDetailsDto);
                    }
                    return Result<DietDetailsGetDTO>.Success(dietDetailsDto);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DietDetailsGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}