using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tabs
{
    public class TabDetails
    {
        public class Query : IRequest<Tab>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Tab>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Tab> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Tabs.FindAsync(request.Id);
            }
        }
    }
}
