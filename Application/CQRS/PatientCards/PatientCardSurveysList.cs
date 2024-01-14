using Application.Core;
using Application.DTOs.PatientsCardsSurveys;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.PatientCards
{
    public class PatientCardSurveysList
    {
        public class Query : IRequest<Result<List<PatientCardSurveyGetDTO>>>
        {
            public int DieteticianId { get; set; }
            public int PatientCardId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<PatientCardSurveyGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<PatientCardSurveyGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var patientSurveys = await _context.PatientCardSurveysDb
                    .Where(m => m.DieticianId == request.DieteticianId && m.PatientCardId==request.PatientCardId)
                    .Select(m => new PatientCardSurveyGetDTO
                    {
                        SurveyId = m.SurveyId,
                        MeasureTime=m.Survey.MeasureTime,
                        Heigth=m.Survey.Heigth,
                        Weith=m.Survey.Weith,
                    })
                    .ToListAsync(cancellationToken);

                    return Result<List<PatientCardSurveyGetDTO>>.Success(patientSurveys);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<PatientCardSurveyGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}
