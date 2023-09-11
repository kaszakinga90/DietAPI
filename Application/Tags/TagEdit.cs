using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tags
{
    public class TagEdit
    {
        public class Command : IRequest
        {
            public Tag Tag { get; set; }
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
                var Tag = await _context.Tags.FindAsync(request.Tag.Id);
                _mapper.Map(request.Tag, Tag);
                await _context.SaveChangesAsync();
            }
        }
    }
}
