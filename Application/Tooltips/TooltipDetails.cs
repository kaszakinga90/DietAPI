using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.ManualPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tooltips
{
    public class TooltipDetails
    {
        public class Query : IRequest<Tooltip>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Tooltip>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Tooltip> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Tooltips.FindAsync(request.Id);
            }
        }
    }
}
