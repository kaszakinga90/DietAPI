using Application.Core;
using Application.DTOs.DieteticianPatientDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.DieticiansPatients
{
    public class FromPatientToDieteticianList
    {
        public class Query : IRequest<Result<List<DieteticianPatientGetDTO>>>
        {
            public int PatientId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<DieteticianPatientGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DieteticianPatientGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dietPatientList = await _context.DieticianPatientsDb
                    .Where(d => d.PatientId == request.PatientId)
                    .Select(d => new DieteticianPatientGetDTO
                    {
                        PatientId = d.PatientId,
                        DieticianId = d.DieticianId,
                        DieteticianName = d.Dietician.FirstName + " " + d.Dietician.LastName,
                        PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                    })
                    .ToListAsync(cancellationToken);

                    return Result<List<DieteticianPatientGetDTO>>.Success(dietPatientList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DieteticianPatientGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}