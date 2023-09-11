using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tabs
{
    public class TabEdit
    {
        public class Command : IRequest
        {
            public Tab Tab { get; set; }
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
                var Tab = await _context.Tabs.FindAsync(request.Tab.Id);
                _mapper.Map(request.Tab, Tab);
                await _context.SaveChangesAsync();
            }
        }
    }
}
