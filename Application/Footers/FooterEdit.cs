using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Footers
{
    public class FooterEdit
    {
        public class Command : IRequest
        {
            public Footer Footer { get; set; }
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
                var Footer = await _context.Footers.FindAsync(request.Footer.Id);
                _mapper.Map(request.Footer, Footer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
