using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Links
{
    public class LinkDetails
    {
        public class Query : IRequest<Link>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Link>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Link> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Links.FindAsync(request.Id);
            }
        }
    }
}
