using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Footers
{
    public class FooterDetails
    {
        public class Query : IRequest<Footer>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Footer>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Footer> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Footers.FindAsync(request.Id);
            }
        }
    }
}
