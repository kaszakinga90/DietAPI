using Application.Core;
using Application.DTOs.DishDetailsToEditDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dishes.DishToEdit.Gets
{
    public class DishRecipeDetails
    {
        public class Query : IRequest<Result<DishRecipeDetailsGetEditDTO>>
        {
            public int DishId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DishRecipeDetailsGetEditDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<DishRecipeDetailsGetEditDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dish = await _context.DishesDb
                        .Include(dish => dish.Recipe)
                            .ThenInclude(recipe => recipe.Steps)
                        .FirstOrDefaultAsync(d => d.Id == request.DishId);

                    if (dish == null)
                    {
                        return Result<DishRecipeDetailsGetEditDTO>.Failure("Nie znaleziono dania o takim Id");
                    }
                    int counter = 1;
                    var dishRecipeDetailsDTO = new DishRecipeDetailsGetEditDTO
                    {
                        Id = 1,
                        RecipeId = (int)dish.RecipeId,
                        RecipeStepsDTO = dish.Recipe.Steps.Select(rs => new RecipeStepGetEditDTO
                        {
                            Id = counter++,
                            StepNumber = rs.StepNumber,
                            Description = rs.Description
                        }).ToList()
                };
                    return Result<DishRecipeDetailsGetEditDTO>.Success(dishRecipeDetailsDTO);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DishRecipeDetailsGetEditDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}
