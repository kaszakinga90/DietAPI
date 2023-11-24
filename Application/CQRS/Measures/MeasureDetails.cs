using DietDB;
using MediatR;
using ModelsDB;

namespace Application.CQRS.Measures
{
    public class MeasureDetails
    {
        public class Query : IRequest<Measure>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Measure>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Measure> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.MeasuresDb.FindAsync(request.Id);
            }
        }
    }
}
