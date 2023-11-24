using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;

namespace Application.CQRS.Measures
{
    public class MeasureList
    {
        public class Query : IRequest<List<Measure>> { }

        public class Handler : IRequestHandler<Query, List<Measure>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<Measure>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.MeasuresDb.ToListAsync(cancellationToken);
            }
        }
    }
}
