using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LayoutPhotos
{
    public class LayoutPhotoCreate
    {
        public class Command : IRequest
        {
            public LayoutPhoto LayoutPhoto { get; set; }
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
                _context.LayoutPhotos.Add(request.LayoutPhoto);

                await _context.SaveChangesAsync();
            }
        }
    }
}
