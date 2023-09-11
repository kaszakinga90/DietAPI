using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.ManualPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tooltips
{
    public class TooltipEdit
    {
        public class Command : IRequest
        {
            public Tooltip Tooltip { get; set; }
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
                var Tooltip = await _context.Tooltips.FindAsync(request.Tooltip.Id);
                _mapper.Map(request.Tooltip, Tooltip);
                await _context.SaveChangesAsync();
            }
        }
    }
}
