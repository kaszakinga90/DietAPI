using Application.Core;
using Application.DTOs.UnitDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Units
{
    public class UnitList
    {
        public class Query : IRequest<Result<List<UnitGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<UnitGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<UnitGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var unitList = await _context.UnitsDb
                        .Where(m => m.isActive)
                        .Select(m => new UnitGetDTO
                        {
                            Id = m.Id,
                            Symbol = m.Symbol,
                            Description = m.Description,
                        })
                        .ToListAsync(cancellationToken);

                    return Result<List<UnitGetDTO>>.Success(unitList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<UnitGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}