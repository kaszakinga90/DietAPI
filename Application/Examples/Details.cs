using DietDB;
using MediatR;
using ModelsDB;

namespace Application.Examples
{
    public class Details
    {
        public class Query:IRequest<Example> {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Example>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Example> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Examples.FindAsync(request.Id);
            }
        }
    }
}
