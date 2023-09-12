using AutoMapper;
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
    public class CarouselEdit
    {
        public class Command : IRequest
        {
            public Carousel Carousel { get; set; }
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
                var Carousel = await _context.Carousels.FindAsync(request.Carousel.Id);
                _mapper.Map(request.Carousel, Carousel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
