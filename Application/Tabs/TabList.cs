using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tabs
{
    public class TabList
    {
        public class Query : IRequest<List<Tab>> { }

        public class Handler : IRequestHandler<Query, List<Tab>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<Tab>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Tabs.ToListAsync(cancellationToken);
            }
        }
    }
}
