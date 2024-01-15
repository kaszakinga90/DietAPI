using Application.Core;
using Application.DTOs.SpecializationDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Patients
{
    public class PatientMessageNoPaginationList
    {
        public class Query : IRequest<Result<List<MessageToDTO>>>
        {
            public int PatientId { get; set; }
            public class Handler : IRequestHandler<Query, Result<List<MessageToDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<List<MessageToDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var patientMessagelist = await _context.MessageToDb
                        .Where(m => m.PatientId == request.PatientId && m.AdminId == null)
                        .Select(m => new MessageToDTO
                        {
                            Id = m.Id,
                            Title = m.Title,
                            Description = m.Description,
                            DieticianId = m.DieticianId,
                            PatientId = m.PatientId,
                            DieticianName = m.Dietician.FirstName + " " + m.Dietician.LastName,
                            AdminName = m.Admin.FirstName + " " + m.Admin.LastName,
                            PatientName = m.Patient.FirstName + " " + m.Patient.LastName,
                            dateAdded = m.dateAdded,
                            ReadDate = m.ReadDate,
                            IsRead = m.IsRead
                        })
                        .ToListAsync(cancellationToken);

                        return Result<List<MessageToDTO>>.Success(patientMessagelist);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<List<MessageToDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}