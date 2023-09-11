using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SubTabs
{
    public class SubTabList
    {
        public class Query : IRequest<List<SubTab>> { }

        public class Handler : IRequestHandler<Query, List<SubTab>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<SubTab>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.SubTabs.ToListAsync(cancellationToken);
            }
        }
    }
}
