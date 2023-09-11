using AutoMapper;
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
    public class SocialMediaEdit
    {
        public class Command : IRequest
        {
            public SocialMedia SocialMedia { get; set; }
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
                var SocialMedia = await _context.SocialMedias.FindAsync(request.SocialMedia.Id);
                _mapper.Map(request.SocialMedia, SocialMedia);
                await _context.SaveChangesAsync();
            }
        }
    }
}
