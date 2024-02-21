using Application.Core;
using Application.DTOs.PatientCardDTO;
using Application.FiltersExtensions.PatientsCards;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.PatientCards
{
    public class PatientCardForPatientList
    {
        public class Query : IRequest<Result<PagedList<PatientCardGetDTO>>>
        {
            public int PatientId { get; set; }
            public PatientCardParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<PatientCardGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<PatientCardGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var patientCardList = _context.PatientCardsDb
                    .Where(m => m.PatientId == request.PatientId)
                    .Select(m => new PatientCardGetDTO
                    {
                        Id = m.Id,
                        PatientId = m.PatientId,
                        DieticianName = m.Dietician.FirstName + " " + m.Dietician.LastName,
                    })
                    .AsQueryable();
                    patientCardList = patientCardList.Search(request.Params.SearchTerm);
                    patientCardList = patientCardList.SearchByDietician(request.Params.SearchTermByDietician);
                    return Result<PagedList<PatientCardGetDTO>>.Success(
                        await PagedList<PatientCardGetDTO>.CreateAsync(patientCardList, request.Params.PageNumber, request.Params.PageSize)
                        );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<PatientCardGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}