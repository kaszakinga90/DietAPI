using Application.Core;
using Application.DTOs.MeasureDTO;
using Application.DTOs.UnitDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                var unitList = await _context.UnitsDb
                    .Select(m => new UnitGetDTO
                    {
                        Id = m.Id,
                        Symbol = m.Symbol,
                        Description = m.Description,
                    })
                    .ToListAsync();
                return Result<List<UnitGetDTO>>.Success(unitList);
            }
        }
    }
}
