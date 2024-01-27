using Application.Core;
using Application.DTOs.IngredientDTO;
using Application.FiltersExtensions.Ingredients;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Ingredients
{
    public class IngredientONLYDieticianList
    {
        public class Query : IRequest<Result<PagedList<IngredientGetDTO>>>
        {
            public int DieticianId { get; set; }
            public IngredientsParams Params { get; set; }

            public class Handler : IRequestHandler<Query, Result<PagedList<IngredientGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<PagedList<IngredientGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var ingridientList = _context.IngredientsDb
                         .Where(i => i.DieticianId == request.DieticianId)
                         .Select(i => new IngredientGetDTO
                         {
                             Id = i.Id,
                             Name = i.Name,
                             Calories = i.Calories,
                             GlycemicIndex = i.GlycemicIndex ?? 0,
                             ServingQuantity = i.ServingQuantity ?? 0,
                             MeasureId = i.MeasureId,
                             UnitId = i.UnitId,
                         })
                        .AsQueryable();

                        ingridientList = ingridientList.Search(request.Params.SearchTerm);
                        return Result<PagedList<IngredientGetDTO>>.Success(await PagedList<IngredientGetDTO>.CreateAsync(ingridientList, request.Params.PageNumber, request.Params.PageSize));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<PagedList<IngredientGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}