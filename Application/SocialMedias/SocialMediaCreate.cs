using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SocialMedias
{
    public class SocialMediaCreate
    {
        public class Command : IRequest
        {
            public SocialMedia SocialMedia { get; set; }
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
                _context.SocialMedias.Add(request.SocialMedia);

                await _context.SaveChangesAsync();
            }
        }
    }
}
