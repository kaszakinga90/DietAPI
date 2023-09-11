using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles
{
    public class ArticleList
    {
        public class Query : IRequest<List<Article>> { }

        public class Handler : IRequestHandler<Query, List<Article>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<Article>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Articles.ToListAsync(cancellationToken);
            }
        }
    }
}
