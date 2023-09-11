using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles
{
    public class ArticleDetails
    {
        public class Query : IRequest<Article>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Article>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Article> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Articles.FindAsync(request.Id);
            }
        }
    }
}
