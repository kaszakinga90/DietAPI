using Application.Core;
using Application.DTOs.DieteticianPatientDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.DietsForPatients
{
    public class DietsForDieticianList
    {
        public class Query : IRequest<Result<PagedList<DietsForDieticianDTO>>>
        {
            public int DieticianId { get; set; }
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<DietsForDieticianDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<DietsForDieticianDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dietsList = _context.DieticianPatientsDb
                    .Where(d => d.DieticianId == request.DieticianId)
                    .Include(d => d.Patient)
                    .Include(d => d.Diet)
                    .Select(d => new DietsForDieticianDTO
                    {
                        Name = d.Diet.Name,
                        PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                        Period = d.Diet.StartDate.Date.ToShortDateString() + " - " + d.Diet.EndDate.Date.ToShortDateString(),
                    })
                    .AsQueryable();

                if (dietsList == null)
                {
                    return Result<PagedList<DietsForDieticianDTO>>.Failure("no results.");
                }

                return Result<PagedList<DietsForDieticianDTO>>.Success(
                    await PagedList<DietsForDieticianDTO>.CreateAsync(dietsList, request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}
