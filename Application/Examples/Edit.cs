using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Examples
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Example Example { get; set; }
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
                var example=await _context.Examples.FindAsync(request.Example.Id);
                _mapper.Map(request.Example, example);
                await _context.SaveChangesAsync();
            }
        }
    }
}
