using Application.Core;
using Application.FiltersExtensions.Diets;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.DietsForPatients
{
    public class DietsForPatientNoPaginationList
    {
        public class Query : IRequest<Result<List<DietGetDTO>>>
        {
            public int PatientId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<DietGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DietGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dietIds = await _context.DietPatientsDb
                        .Where(dp => dp.PatientId == request.PatientId)
                        .Select(dp => dp.DietId)
                        .ToListAsync(cancellationToken);

                    var dietsList = await _context.DietsDb
                        .Where(d => dietIds.Contains(d.Id))
                        .Include(d => d.Dietician)
                        .Select(d => new DietGetDTO
                        {
                            Id = d.Id,
                            Name = d.Name,
                            DieteticanName = d.Dietician.FirstName + " " + d.Dietician.LastName,
                            StartDate = d.StartDate.Date,
                            EndDate = d.EndDate.Date,
                        })
                        .ToListAsync(cancellationToken);

                    return Result<List<DietGetDTO>>.Success(dietsList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DietGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }

        }
    }
}