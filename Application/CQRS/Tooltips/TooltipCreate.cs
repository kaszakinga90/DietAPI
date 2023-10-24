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
    public class TooltipCreate
    {
        public class Command : IRequest
        {
            public Tooltip Tooltip { get; set; }
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
                _context.Tooltips.Add(request.Tooltip);

                await _context.SaveChangesAsync();
            }
        }
    }
}
