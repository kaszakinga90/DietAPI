using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SocialMedias
{
    public class SocialMediaList
    {
        public class Query : IRequest<List<SocialMedia>> { }

        public class Handler : IRequestHandler<Query, List<SocialMedia>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<SocialMedia>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.SocialMedias.ToListAsync(cancellationToken);
            }
        }
    }
}
