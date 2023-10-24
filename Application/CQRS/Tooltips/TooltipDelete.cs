using DietDB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Tooltips
{
    public class TooltipDelete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
<<<<<<< HEAD:Application/CQRS/Tooltips/TooltipDelete.cs
                var example = await _context.Tooltips.FindAsync(request.Id);
=======
                var example=await _context.Examples.FindAsync(request.Id);

>>>>>>> 6d047e7594153c95fdeecf218c13172cd7de5b09:Application/Examples/Delete.cs

                _context.Remove(example);

                await _context.SaveChangesAsync();
            }
        }
    }
}
