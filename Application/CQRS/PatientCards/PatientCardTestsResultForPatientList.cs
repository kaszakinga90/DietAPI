using Application.Core;
using Application.DTOs.TestsResultsDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.PatientCards
{
    public class PatientCardTestsResultForPatientList
    {
        public class Query : IRequest<Result<List<TestResultGetDTO>>>
        {
            public int PatientCardId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<TestResultGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<TestResultGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var patientTestResults = await _context.TestResultsDb
                    .Where(m => m.PatientCardId == request.PatientCardId && m.isActive == true)
                    .Select(m => new TestResultGetDTO
                    {
                        Id = m.Id,
                        TestDate = m.SingleTestResults.TestDate,
                        test1 = m.SingleTestResults.test1,
                        test2 = m.SingleTestResults.test2,
                        test3 = m.SingleTestResults.test3,
                    })
                    .ToListAsync(cancellationToken);

                    return Result<List<TestResultGetDTO>>.Success(patientTestResults);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<TestResultGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}