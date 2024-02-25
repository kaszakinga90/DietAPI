using Application.Core;
using Application.DTOs.PatientCardDTO;
using Application.FiltersExtensions.PatientMessages;
using Application.FiltersExtensions.PatientsCards;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.PatientCards
{
    public class PatientCardList
    {
        public class Query : IRequest<Result<PagedList<PatientCardGetDTO>>>
        {
            public int DieticianId { get; set; }
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
                          .Where(m => m.DieticianId == request.DieticianId && m.isActive)
                          .Select(m => new PatientCardGetDTO
                          {
                              Id = m.Id,
                              PatientId = m.PatientId,
                              PatientName = m.Patient.FirstName + " " + m.Patient.LastName,
                          })
                          .AsQueryable();

                    patientCardList = patientCardList.Search(request.Params.SearchTerm);
                    patientCardList = patientCardList.PatientCardSort(request.Params.OrderBy);
                    patientCardList = patientCardList.PatientCardFilter(request.Params.PatientNames);

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