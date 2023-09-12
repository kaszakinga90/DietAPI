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
    public class CarouselCreate
    {
        public class Command : IRequest
        {
            public Carousel Carousel { get; set; }
        }
        public class Hendler : IRequestHandler<Command>
        {
            private readonly DietContext _context;

            public Hendler(DietContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Carousels.Add(request.Carousel);

                await _context.SaveChangesAsync();
            }
        }
    }
}
