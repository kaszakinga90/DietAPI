using Application.Core;
using Application.DTOs.GenericsDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.DietForPatients
{
    public class DietForPatientToDocumentCreateDetails
    {
        public class Command : IRequest<Result<DietForPatientToDocumentDTO>>
        {
            public int DieticianId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<DietForPatientToDocumentDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<DietForPatientToDocumentDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var dietForPatient = await _context.DietsDb
                    .Where(m => m.DieteticianId == request.DieticianId)
                    .Include(diet => diet.Patient)
                    .Include(d => d.Dietician)
                    .Select(d => new DietForPatientToDocumentDTO
                    {
                        Name = d.Name,
                        PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                        DieticianName = d.Dietician.FirstName + " " + d.Dietician.LastName,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        numberOfMeals = d.numberOfMeals,
                        Period = (int)(d.EndDate - d.StartDate).TotalDays

                    })
                    .FirstOrDefaultAsync();

                if (dietForPatient == null)
                {
                    return Result<DietForPatientToDocumentDTO>.Failure("cannot create diet for patient to document.");
                }

                return Result<DietForPatientToDocumentDTO>.Success(dietForPatient);
            }
        }
    }
}
