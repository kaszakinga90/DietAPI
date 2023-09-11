using AutoMapper;
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
    public class ArticleEdit
    {
        public class Command : IRequest
        {
            public Article Article { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var Article = await _context.Articles.FindAsync(request.Article.Id);
                _mapper.Map(request.Article, Article);
                await _context.SaveChangesAsync();
            }
        }
    }
}
