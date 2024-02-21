using Application.Core;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Patients
{
    public class PatientsListNoPagination
    {
        public class Query : IRequest<Result<List<PatientGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<PatientGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<PatientGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var stateList = await _context.PatientsDb
                          .Select(m => new PatientGetDTO
                          {
                              Id = m.Id,
                              PatientName = m.FirstName+" "+m.LastName,
                          })
                          .ToListAsync(cancellationToken);

                    return Result<List<PatientGetDTO>>.Success(stateList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<PatientGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}