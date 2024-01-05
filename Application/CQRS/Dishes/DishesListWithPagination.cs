using Application.Core;
using Application.DTOs.DishDTO;
using Application.FiltersExtensions.Dishes;
using DietDB;
using MediatR;

namespace Application.CQRS.Dishes
{
    public class DishesListWithPagination
    {
        public class Query : IRequest<Result<PagedList<DishGetDTO>>>
        {
            public int DieteticianId { get; set; }
            public DishParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<DishGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<DishGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dishesList = _context.DishesDb
                    .Where(d => d.DieticianId == null || d.DieticianId == request.DieteticianId)
                    .Select(d => new DishGetDTO
                    {
                        Id = d.Id,
                        Name = d.Name,
                    })
                    .AsQueryable();

                if (dishesList == null)
                {
                    return Result<PagedList<DishGetDTO>>.Failure("no results");
                }

                dishesList = dishesList.DishSearch(request.Params.SearchTerm);
                dishesList = dishesList.DishSort(request.Params.OrderBy);
                dishesList = dishesList.DishFilter(request.Params.DishNames);

                return Result<PagedList<DishGetDTO>>.Success(
                    await PagedList<DishGetDTO>.CreateAsync(dishesList, request.Params.PageNumber, request.Params.PageSize)
                    );
            }
        }
    }
}
