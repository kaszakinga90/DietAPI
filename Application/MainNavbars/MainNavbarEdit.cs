using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainNavbars
{
    public class MainNavbarEdit
    {
        public class Command : IRequest
        {
            public MainNavbar MainNavbar { get; set; }
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
                var MainNavbar = await _context.MainNavbars.FindAsync(request.MainNavbar.Id);
                _mapper.Map(request.MainNavbar, MainNavbar);
                await _context.SaveChangesAsync();
            }
        }
    }
}
