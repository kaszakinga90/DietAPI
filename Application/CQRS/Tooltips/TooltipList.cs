using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.ManualPanel;

namespace Application.CQRS.Tooltips
{
    public class TooltipList
    {
        public class Query : IRequest<List<Tooltip>> { }

        public class Handler : IRequestHandler<Query, List<Tooltip>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<Tooltip>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Tooltips.ToListAsync(cancellationToken);
            }
        }
    }
}
