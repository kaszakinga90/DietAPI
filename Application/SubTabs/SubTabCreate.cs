using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SubTabs
{
    public class SubTabCreate
    {
        public class Command : IRequest
        {
            public SubTab SubTab { get; set; }
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
                _context.SubTabs.Add(request.SubTab);

                await _context.SaveChangesAsync();
            }
        }
    }
}
