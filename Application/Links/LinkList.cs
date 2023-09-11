using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Links
{
    public class LinkList
    {
        public class Query : IRequest<List<Link>> { }

        public class Handler : IRequestHandler<Query, List<Link>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<Link>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Links.ToListAsync(cancellationToken);
            }
        }
    }
}
