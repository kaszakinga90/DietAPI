using Application.Core;
using Application.FiltersExtensions.Diets;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Diets
{
    public class DietList
    {
        public class Query : IRequest<Result<PagedList<DietGetDTO>>>
        {
            public int DieticianId { get; set; }
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
                try
                {
                    var dietsList = _context.DietsDb
                    .Where(d => d.DieteticianId == request.DieticianId && d.isActive)
                    .Select(d => new DietGetDTO
                    {
                        Id = d.Id,
                        DieteticianId = d.DieteticianId,
                        Name = d.Name,
                        StartDate = d.StartDate.Date,
                        EndDate = d.EndDate.Date,
                        numberOfMeals = d.numberOfMeals,
                        PatientId = d.PatientId,
                        PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                        DieteticanName = d.Dietician.FirstName + " " + d.Dietician.LastName,
                    })
                    .AsQueryable();

                    dietsList = dietsList.DietSort(request.Params.OrderBy);
                    dietsList = dietsList.DietFilter(request.Params.PatientNames);
                    dietsList = dietsList.DietSearch(request.Params.SearchTerm);

                    return Result<PagedList<DietGetDTO>>.Success(
                        await PagedList<DietGetDTO>.CreateAsync(dietsList, request.Params.PageNumber, request.Params.PageSize)
                        );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<DietGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}