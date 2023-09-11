using DietDB;
using MediatR;
using ModelsDB.Layout;
using ModelsDB.ManualPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Carousels
{
    public class CarouselDetails
    {
        public class Query : IRequest<Carousel>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Carousel>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Carousel> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Carousels.FindAsync(request.Id);
            }
        }
    }
}
