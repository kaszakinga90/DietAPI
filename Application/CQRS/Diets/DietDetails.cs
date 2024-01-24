// TODO : whole class
using Application.DTOs.DietDTO;
using MediatR;
using AutoMapper;
using DietDB;
using Application.Core;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.DTOs.RecipeDTO;
using Application.DTOs.RecipeStepDTO;
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
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
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
                                     .ThenInclude(dish => dish.Recipe)
                                     .ThenInclude(recipe => recipe.Steps)
                             .FirstOrDefaultAsync(d => d.Id == request.DietId);

                    if (diet == null)
                    {
                        return Result<DietDetailsGetDTO>.Failure("Nie znaleziono diety");
                    }

                    var dietDetailsDto = new DietDetailsGetDTO
                    {
                        Name = diet.Name,
                        StartDate = diet.StartDate,
                        EndDate = diet.EndDate,
                        PatientName = diet.Patient?.Email,
                        DieticianName = diet.Dietician?.Email,
                        numberOfMeals = diet.numberOfMeals,
                        MealTimeToDietDetailsGetDTO = new List<MealTimeToDietDetailsGetDTO>(),
                    };

                    foreach (var mealTimeToXYAxis in diet.MealTimesToXYAxis)
                    {
                        var mealTimeToDietDetailsDto = new MealTimeToDietDetailsGetDTO
                        {
                            Id = mealTimeToXYAxis.Id,
                            MealId = mealTimeToXYAxis.MealId,
                            // Mapuj inne właściwości

                            DishGetDTO = new DishGetDTO
                            {
                                Id = mealTimeToXYAxis.Dish.Id,
                                Name = mealTimeToXYAxis.Dish?.Name,
                                // Mapuj inne właściwości
                                DishIngredients = mealTimeToXYAxis.Dish?.DishIngredients?.Select(di => new DishIngredientGetDTO
                                {
                                    DishId = di.DishId,
                                    Name = di.Ingredient?.Name,
                                    IngredientId = di.IngredientId,
                                    IngredientName = di.Ingredient?.Name,
                                    Quantity = di.Quantity,
                                    UnitId = di.UnitId
                                    // Mapuj inne właściwości
                                }).ToList(),

                                RecipeStepsDTO = mealTimeToXYAxis.Dish?.Recipe?.Steps?.Select(step => new RecipeStepGetDTO
                                {
                                    Id = step.Id,
                                    StepNumber = step.StepNumber,
                                    Description = step.Description
                                    // Mapuj inne właściwości kroku
                                }).ToList()
                            }
                        };

                        dietDetailsDto.MealTimeToDietDetailsGetDTO.Add(mealTimeToDietDetailsDto);
                    }
                    //var dietDetailsDTO = _mapper.Map<DietDetailsGetDTO>(diet);
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