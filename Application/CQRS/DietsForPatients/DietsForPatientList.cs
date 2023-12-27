using Application.Core;
using Application.DTOs.DieteticianPatientDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.DietsForPatients
{
    public class DietsForPatientList
    {
        public class Query : IRequest<Result<PagedList<DietsForPatientDTO>>>
        {
            public int PatientId { get; set; }
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<DietsForPatientDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<DietsForPatientDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dietsList = _context.DieticianPatientsDb
                    .Where(d => d.PatientId == request.PatientId)
                    .Include(d => d.Dietician)
                    .Include(d => d.Diet)
                    .Select(d => new DietsForPatientDTO
                     {
                         Name = d.Diet.Name,
                         DieticianName = d.Dietician.FirstName + " " + d.Dietician.LastName,
                         Period = d.Diet.StartDate.Date.ToShortDateString() + " - " + d.Diet.EndDate.Date.ToShortDateString(),
                     })
                    .AsQueryable();

                if (dietsList == null)
                {
                    return Result<PagedList<DietsForPatientDTO>>.Failure("no results.");
                }

                return Result<PagedList<DietsForPatientDTO>>.Success(
                    await PagedList<DietsForPatientDTO>.CreateAsync(dietsList, request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}
