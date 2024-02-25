using Application.Core;
using Application.DTOs.CountryStateDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.CountryStates
{
    public class CountryStateList
    {
        public class Query : IRequest<Result<List<CountryStateGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<CountryStateGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<CountryStateGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var stateList = await _context.CountryStatesDb
                          .Where(m => m.isActive)
                          .Select(m => new CountryStateGetDTO
                          {
                              Id = m.Id,
                              StateName = m.StateName,
                          })
                          .ToListAsync(cancellationToken);

                    return Result<List<CountryStateGetDTO>>.Success(stateList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<CountryStateGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}