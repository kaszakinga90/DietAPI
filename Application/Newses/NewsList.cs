using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Newses
{
    public class NewsList
    {
        public class Query : IRequest<List<News>> { }

        public class Handler : IRequestHandler<Query, List<News>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<News>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Newses.ToListAsync(cancellationToken);
            }
        }
    }
}
