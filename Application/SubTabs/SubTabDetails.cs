using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SubTabs
{
    public class SubTabDetails
    {
        public class Query : IRequest<SubTab>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, SubTab>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<SubTab> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.SubTabs.FindAsync(request.Id);
            }
        }
    }
}
