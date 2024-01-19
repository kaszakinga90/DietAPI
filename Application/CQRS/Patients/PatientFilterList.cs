using Application.Core;
using Application.DTOs.MessagesDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Patients
{
    public class PatientFilterList
    {
        public class Query : IRequest<Result<PatientFiltersDTO>> { }

        public class Handler : IRequestHandler<Query, Result<PatientFiltersDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PatientFiltersDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var filters = new PatientFiltersDTO
                {
                    DatesAdded = await _context.PatientsDb
                        .Select(m => m.dateAdded)
                        .Distinct()
                        .ToListAsync(cancellationToken),

                    PatientNames = await _context.PatientsDb
                        .Select(m => m.FirstName + " " + m.LastName)
                        .Distinct()
                        .ToListAsync(cancellationToken)
                };
                return Result<PatientFiltersDTO>.Success(filters);
            }
        }
    }
}