using Application.Core;
using Application.FiltersExtensions.Diets;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.DietsForPatients
{
    public class DietsForPatientList
    {
        public class Query : IRequest<Result<PagedList<DietGetDTO>>>
        {
            public int PatientId { get; set; }
            public DietParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<DietGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<DietGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dietsList = _context.DietsDb
                    .Where(d => d.PatientId == request.PatientId)
                    .Include(d => d.Dietician)
                    .Select(d => new DietGetDTO
                    {
                        Id = d.Id,
                        Name = d.Name,
                        DieteticanName = d.Dietician.FirstName + " " + d.Dietician.LastName,
                        StartDate = d.StartDate.Date,
                        EndDate = d.EndDate.Date,
                    })
                    .AsQueryable();

                dietsList = dietsList.DietSortByDietician(request.Params.OrderBy);
                dietsList = dietsList.DietFilterDietician(request.Params.DieticianNames);
                dietsList = dietsList.DietSearch(request.Params.SearchTerm);

                if (dietsList == null)
                {
                    return Result<PagedList<DietGetDTO>>.Failure("no results.");
                }

                return Result<PagedList<DietGetDTO>>.Success(
                    await PagedList<DietGetDTO>.CreateAsync(dietsList, request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}
