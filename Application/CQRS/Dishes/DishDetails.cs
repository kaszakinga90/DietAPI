using Application.Core;
using Application.DTOs.DishDTO;
using Application.DTOs.DishFoodCatalogDTO;
using Application.DTOs.DishIngredientDTO;
using Application.DTOs.RecipeStepDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dishes
{
    public class DishDetails
    {
        public class Query : IRequest<Result<DishGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DishGetDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<DishGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dish = await _context.DishesDb
                        .Include(dish => dish.Recipe)
                        .ThenInclude(recipe => recipe.Steps)
                        .Include(d => d.DishIngredients)
                            .ThenInclude(di => di.Ingredient)
                                .ThenInclude(i => i.Measure)
                        .Include(d => d.DishFoodCatalogs)
                            .ThenInclude(df => df.FoodCatalog)
                        .FirstOrDefaultAsync(d => d.Id == request.Id);

                    if (dish == null)
                    {
                        return Result<DishGetDTO>.Failure("Nie znaleziono dania o takim Id");
                    }
                    var recipeSteps = await _context.RecipeStepsDb
               .Where(r => r.RecipeId == dish.Id)
               .ToListAsync();

                    var dishDTO = new DishGetDTO
                    {
                        Id = dish.Id,
                        Name = dish.Name,
                        Calories = dish.Calories,
                        ServingQuantity = dish.ServingQuantity,
                        MeasureId = (int)dish.MeasureId,
                        UnitId = (int)dish.UnitId,
                        GlycemicIndex = dish.GlycemicIndex,
                        PreparingTime = dish.PreparingTime,
                        RecipeId = (int)dish.RecipeId,
                        DieteticianId = dish.DieticianId,
                        DishIngredients = dish.DishIngredients.Select(di => new DishIngredientGetDTO
                        {
                            DishId = di.DishId,
                            Name = di.Ingredient.Name,
                            IngredientId = di.IngredientId,
                            IngredientName = di.Ingredient.Name,
                            Quantity = di.Quantity,
                            UnitId = di.UnitId
                        }).ToList(),
                        DishFoodCatalogs = dish.DishFoodCatalogs.Select(df => new DishFoodCatalogGetDTO
                        {
                            Id = df.Id,
                            DishId = df.DishId,
                            DishName = dish.Name,
                            FoodCatalogId = df.FoodCatalogId,
                            FoodCatalogName = df.FoodCatalog.CatalogName
                        }).ToList(),
                        RecipeStepsDTO = dish.Recipe.Steps.Select(rs => new RecipeStepGetDTO
                        {
                            Id = rs.Id,
                            StepNumber = rs.StepNumber,
                            Description = rs.Description,
                            RecipeId = rs.RecipeId
                        }).ToList()
                    };

                    return Result<DishGetDTO>.Success(dishDTO);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DishGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }

        }
    }
}