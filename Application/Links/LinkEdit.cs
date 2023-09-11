using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Links
{
    public class LinkEdit
    {
        public class Command : IRequest
        {
            public Link Link { get; set; }
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
                var Link = await _context.Links.FindAsync(request.Link.Id);
                _mapper.Map(request.Link, Link);
                await _context.SaveChangesAsync();
            }
        }
    }
}
