using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LayoutPhotos
{
    public class LayoutPhotoDetails
    {
        public class Query : IRequest<LayoutPhoto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, LayoutPhoto>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<LayoutPhoto> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.LayoutPhotos.FindAsync(request.Id);
            }
        }
    }
}
