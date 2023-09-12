using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tags
{
    public class TagList
    {
        public class Query : IRequest<List<Tag>> { }

        public class Handler : IRequestHandler<Query, List<Tag>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<Tag>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Tags.ToListAsync(cancellationToken);
            }
        }
    }
}
