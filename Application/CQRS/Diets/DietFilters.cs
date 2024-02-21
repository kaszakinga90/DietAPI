using Application.Core;
using Application.DTOs.DietDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Diets
{
    public class DietFilters
    {
        public class Query : IRequest<Result<DietFiltersDTO>>
        {
            public int DieticianId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DietFiltersDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<DietFiltersDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var filters = new DietFiltersDTO
                {
                    DatesAdded = await _context.DietsDb
                        .Where(m => m.DieteticianId == request.DieticianId && m.DieteticianId!=null)
                        .Select(m => m.dateAdded)
                        .Distinct()
                        .ToListAsync(cancellationToken),

                    PatientNames = await _context.DietsDb
                        .Where(m => m.DieteticianId == request.DieticianId && m.PatientId != null)
                        .Select(m => m.Patient.FirstName + " " + m.Patient.LastName)
                        .Distinct()
                        .ToListAsync(cancellationToken)
                };
                return Result<DietFiltersDTO>.Success(filters);
            }
        }
    }
}