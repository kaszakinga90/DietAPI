﻿using Application.Core;
using Application.DTOs.DieteticianPatientDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.DieticiansPatients
{
    public class FromDieticianToPatientList
    {
        public class Query : IRequest<Result<List<DieteticianPatientDTO>>>
        {
            public int DieticianId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<DieteticianPatientDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DieteticianPatientDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dietPatientList = await _context.DieticianPatientsDb
                          .Where(d => d.DieticianId == request.DieticianId)
                          .Select(d => new DieteticianPatientDTO
                          {
                              PatientId = d.PatientId,
                              DieticianId = d.DieticianId,
                              DieteticianName = d.Dietician.FirstName + " " + d.Dietician.LastName,
                              PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                          })
                          .ToListAsync(cancellationToken);

                    return Result<List<DieteticianPatientDTO>>.Success(dietPatientList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DieteticianPatientDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}