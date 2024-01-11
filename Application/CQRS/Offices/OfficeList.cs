using Application.Core;
using Application.DTOs.DieticianOfficeDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Offices
{
    public class OfficeList
    {
        public class Query : IRequest<Result<List<DieticianOfficesGetDTO>>>
        {
            public int DieteticianId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<DieticianOfficesGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DieticianOfficesGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var offices = await _context.DieticianOffices
                    .Where(m => m.DieticianId == request.DieteticianId)
                    .Select(m => new DieticianOfficesGetDTO
                    {
                        Id = m.OfficeId,
                        OfficeName = m.Office.OfficeName,
                    })
                    .ToListAsync(cancellationToken);

                    return Result<List<DieticianOfficesGetDTO>>.Success(offices);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DieticianOfficesGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}