using Application.Core;
using Application.DTOs.MeasureDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;

namespace Application.CQRS.Measures
{
    public class MeasureList
    {
        public class Query : IRequest<Result<List<MeasureGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<MeasureGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<MeasureGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var measureList=await _context.MeasuresDb
                    .Select(m=>new MeasureGetDTO
                    {
                        Id = m.Id,
                        Symbol = m.Symbol,
                        Description = m.Description,
                    })
                    .ToListAsync();
                return Result<List<MeasureGetDTO>>.Success(measureList);
            }
        }
    }
}
