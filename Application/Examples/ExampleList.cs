using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;

namespace Application.Examples
{
    public class ExampleList
    {
        public class Query : IRequest<List<Example>> { }

        public class Handler : IRequestHandler<Query, List<Example>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<Example>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Tooltip.ToListAsync(cancellationToken);
            }
        }
    }
}