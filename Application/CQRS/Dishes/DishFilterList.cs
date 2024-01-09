using Application.Core;
using Application.DTOs.DishDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Dishes
{
    public class DishFilterList
    {
        public class Query : IRequest<Result<DishFiltersDTO>>
        {
            public int DishId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DishFiltersDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<DishFiltersDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var filters = new DishFiltersDTO
                {
                    DatesAdded = await _context.DieticiansDb
                        .Select(m => m.dateAdded)
                        .Distinct()
                        .ToListAsync(),
                };

                if (filters == null)
                {
                    return Result<DishFiltersDTO>.Failure("no results");
                }

                return Result<DishFiltersDTO>.Success(filters);
            }
        }
    }
}
