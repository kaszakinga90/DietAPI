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

namespace Application.CQRS.Tooltips
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
<<<<<<< HEAD:Application/CQRS/Tooltips/TooltipEdit.cs
                var Tooltip = await _context.Tooltips.FindAsync(request.Tooltip.Id);
                _mapper.Map(request.Tooltip, Tooltip);
=======
                var example=await _context.Examples.FindAsync(request.Example.Id);
                _mapper.Map(request.Example, example);
>>>>>>> 6d047e7594153c95fdeecf218c13172cd7de5b09:Application/Examples/Edit.cs
                await _context.SaveChangesAsync();
            }
        }
    }
}
