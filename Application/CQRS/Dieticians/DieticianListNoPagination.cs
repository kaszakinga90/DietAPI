using Application.Core;
using Application.DTOs.DieticianDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dieticians
{
    public class DieticianListNoPagination
    {
        public class Query : IRequest<Result<List<DieticianGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<DieticianGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DieticianGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var stateList = await _context.DieticiansDb
                          .Select(m => new DieticianGetDTO
                          {
                              Id = m.Id,
                              DieticianName = m.FirstName+" "+m.LastName,
                          })
                          .ToListAsync(cancellationToken);

                    return Result<List<DieticianGetDTO>>.Success(stateList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DieticianGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}