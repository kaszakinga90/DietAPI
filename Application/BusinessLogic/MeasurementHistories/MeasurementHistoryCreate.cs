using Application.BusinessLogic.CalculatesAndStatistics;
using Application.Core;
using Application.DTOs.ReportsClassesDTO.Reports;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using System.Diagnostics;

namespace Application.BusinessLogic.MeasurementHistories
{
    public class MeasurementHistoryCreate
    {
        public class Command : IRequest<Result<List<MeasurementsHistoryDTO>>>
        {
            public int DieticianId { get; set; }
            public int PatientId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<List<MeasurementsHistoryDTO>>>
        {
            private readonly DietContext _context;
            private readonly CalculatorService _calculator;

            public Handler(DietContext context, CalculatorService calculator)
            {
                _context = context;
                _calculator = calculator;
            }

            public async Task<Result<List<MeasurementsHistoryDTO>>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<Survey> surveyQuery = _context.PatientCardSurveysDb
                    .Include(pcs => pcs.Survey)
                    .Include(p => p.PatientCard)
                    .Where(pcs => pcs.PatientCard.PatientId == request.PatientId && pcs.PatientCard.DieticianId == request.DieticianId && pcs.isActive)
                    .Select(pcs => pcs.Survey);

                    if (request.StartDate != null)
                    {
                        surveyQuery = surveyQuery.Where(survey => survey.MeasureTime >= request.StartDate);
                    }

                    if (request.EndDate != null)
                    {
                        surveyQuery = surveyQuery.Where(survey => survey.MeasureTime <= request.EndDate);
                    }

                    var measurementList = await surveyQuery
                        .Select(mh => new MeasurementsHistoryDTO
                        {
                            Id = 0,
                            MeasureTime = mh.MeasureTime.ToShortDateString(),
                            Height = mh.Heigth,
                            Weight = mh.Weith,
                        })
                        .ToListAsync(cancellationToken);

                    foreach (var measurement in measurementList)
                    {
                        measurement.CalculateBMI();
                    }

                    _calculator.CalculateWeightChange(measurementList);
                    _calculator.CalculateBMIChange(measurementList);

                    int counter = 1;

                    foreach (var item in measurementList)
                    {
                        item.Id = counter++;
                    }

                    return Result<List<MeasurementsHistoryDTO>>.Success(measurementList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<MeasurementsHistoryDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}