using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SocialMedias
{
    public class SocialMediaDetails
    {
        public class Query : IRequest<SocialMedia>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, SocialMedia>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<SocialMedia> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.SocialMedias.FindAsync(request.Id);
            }
        }
    }
}
