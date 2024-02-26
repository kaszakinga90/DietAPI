using Application.Core;
using Application.DTOs.DishDetailsToEditDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dishes.DishToEdit.Gets
{
    public class DishFoodCatalogDetailsList
    {
        public class Query : IRequest<Result<List<DishFoodCatalogsDetailsGetEditDTO>>>
        {
            public int DishId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<DishFoodCatalogsDetailsGetEditDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DishFoodCatalogsDetailsGetEditDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dishFoodCatalogList = await _context.DishFoodCatalogsDb
                        .Include(d => d.FoodCatalog)
                        .Where(d => d.DishId == request.DishId && d.isActive)
                    .Select(d => new DishFoodCatalogsDetailsGetEditDTO
                    {
                        Id = d.Id,
                        DishId = d.DishId,
                        FoodCatalogId = d.FoodCatalogId,
                        FoodCatalogName = d.FoodCatalog.CatalogName
                    })
                    .ToListAsync(cancellationToken);

                    return Result<List<DishFoodCatalogsDetailsGetEditDTO>>.Success(dishFoodCatalogList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DishFoodCatalogsDetailsGetEditDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}