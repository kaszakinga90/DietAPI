using Application.Core;
using Application.DTOs.DishDetailsToEditDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dishes.DishToEdit.Gets
{
    public class DishIngredientsDetailsList
    {
        public class Query : IRequest<Result<List<DishIngredientsDetailsGetEditDTO>>>
        {
            public int DishId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<DishIngredientsDetailsGetEditDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DishIngredientsDetailsGetEditDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dishIngredientsList = await _context.DishIngredientsDb
                        .Include(i => i.Ingredient)
                            .ThenInclude(i => i.Measure)
                        .Where(d => d.DishId == request.DishId && d.isActive)
                        .Select(d => new DishIngredientsDetailsGetEditDTO
                        {
                            Id = 0,
                            DishId = d.DishId,
                            IngredientId = d.IngredientId,
                            IngredientName = d.Ingredient.Name,
                            Quantity = d.Quantity,
                            UnitId = d.UnitId
                        })
                        .ToListAsync(cancellationToken);

                    int counter = 1;

                    foreach (var ingredient in dishIngredientsList)
                    {
                        ingredient.Id = counter++;
                    }

                    return Result<List<DishIngredientsDetailsGetEditDTO>>.Success(dishIngredientsList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DishIngredientsDetailsGetEditDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}