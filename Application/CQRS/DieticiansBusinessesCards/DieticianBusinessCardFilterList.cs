using Application.Core;
using Application.DTOs.DieticianBusinessCardDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.DieticiansBusinessesCards
{
    public class DieticianBusinessCardFilterList
    {
        public class Query : IRequest<Result<BusinessCardFiltersDTO>>
        {
            //public int DieticianId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<BusinessCardFiltersDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<BusinessCardFiltersDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var filters = new BusinessCardFiltersDTO
                {
                    DatesAdded = await _context.DieticiansDb
                        //.Where(m => m.UserId == request.DieticianId && m.isDietician)
                        .Select(m => m.dateAdded)
                        .Distinct()
                        .ToListAsync(cancellationToken),

                    DieticianNames = await _context.DieticiansDb
                        //.Where(m => m.isDietician != null)
                        .Select(m => m.FirstName + " " + m.LastName)
                        .Distinct()
                        .ToListAsync(cancellationToken),

                    SpecializationNames = await _context.SpecializationsDb
                        //.Where(m => m.DieticianId == request.DieticianId && m.DieticianId != null)
                        .Select(m => m.SpecializationName)
                        .Distinct()
                        .ToListAsync(cancellationToken),

                    StateNames = await _context.CountryStatesDb
                        //.Where(m => m.DieticianId == request.DieticianId && m.DieticianId != null)
                        .Select(m => m.StateName)
                        .Distinct()
                        .ToListAsync(cancellationToken),
                };
                return Result<BusinessCardFiltersDTO>.Success(filters);
            }
        }
    }
}