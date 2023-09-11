using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Footers
{
    public class FooterList
    {
        public class Query : IRequest<List<Footer>> { }

        public class Handler : IRequestHandler<Query, List<Footer>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<Footer>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Footers.ToListAsync(cancellationToken);
            }
        }
    }
}
