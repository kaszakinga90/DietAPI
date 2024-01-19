using Application.Core;
using Application.DTOs.DieticianDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Dieticians
{
    public class DieticiansFilterList
    {
        public class Query : IRequest<Result<DieticianFiltersDTO>> { }

        public class Handler : IRequestHandler<Query, Result<DieticianFiltersDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<DieticianFiltersDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var filters = new DieticianFiltersDTO
                {
                    DatesAdded = await _context.DieticiansDb
                        .Select(m => m.dateAdded)
                        .Distinct()
                        .ToListAsync(cancellationToken),

                    DieticianNames = await _context.DieticiansDb
                        .Select(m => m.FirstName + " " + m.LastName)
                        .Distinct()
                        .ToListAsync(cancellationToken)
                };
                return Result<DieticianFiltersDTO>.Success(filters);
            }
        }
    }
}