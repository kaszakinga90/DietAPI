using Application.Core;
using Application.DTOs.DishFoodCatalogDTO;
using Application.FiltersExtensions.DishesFoodCatalog;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Dishes
{
    public class DishFoodCatalogList
    {
        public class Query : IRequest<Result<PagedList<DishFoodCatalogGetDTO>>>
        {
            public int DishId { get; set; }
            public int FoodCatalogId { get; set; }
            public DishesFoodCatalogParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<DishFoodCatalogGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<DishFoodCatalogGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dishesFoodCatalogsList = _context.DishFoodCatalogsDb
                    .Where(d => d.FoodCatalogId == request.FoodCatalogId && d.isActive)
                    .Select(d => new DishFoodCatalogGetDTO
                    {
                        Id = d.Id,
                        DishId=d.DishId,
                        DishName=d.Dish.Name,
                        FoodCatalogId=d.FoodCatalogId,
                        FoodCatalogName=d.FoodCatalog.CatalogName,
                    })
                    .AsQueryable();

                    dishesFoodCatalogsList = dishesFoodCatalogsList.DishFoodCatalogSearch(request.Params.SearchTerm);

                    return Result<PagedList<DishFoodCatalogGetDTO>>.Success(
                        await PagedList<DishFoodCatalogGetDTO>.CreateAsync(dishesFoodCatalogsList, request.Params.PageNumber, request.Params.PageSize)
                        );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<DishFoodCatalogGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}