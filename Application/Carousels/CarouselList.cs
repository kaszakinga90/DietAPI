using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using ModelsDB.ManualPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Carousels
{
    public class CarouselList
    {
        public class Query : IRequest<List<Carousel>> { }

        public class Handler : IRequestHandler<Query, List<Carousel>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<Carousel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Carousels.ToListAsync(cancellationToken);
            }
        }
    }
}
