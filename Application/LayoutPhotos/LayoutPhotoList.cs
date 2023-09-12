using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LayoutPhotos
{
    public class LayoutPhotoList
    {
        public class Query : IRequest<List<LayoutPhoto>> { }

        public class Handler : IRequestHandler<Query, List<LayoutPhoto>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<LayoutPhoto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.LayoutPhotos.ToListAsync(cancellationToken);
            }
        }
    }
}
