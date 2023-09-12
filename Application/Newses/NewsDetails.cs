using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Newses
{
    public class NewsDetails
    {
        public class Query : IRequest<News>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, News>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<News> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Newses.FindAsync(request.Id);
            }
        }
    }
}
