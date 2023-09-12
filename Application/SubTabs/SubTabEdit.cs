using AutoMapper;
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
    public class SubTabEdit
    {
        public class Command : IRequest
        {
            public SubTab SubTab { get; set; }
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
                var SubTab = await _context.SubTabs.FindAsync(request.SubTab.Id);
                _mapper.Map(request.SubTab, SubTab);
                await _context.SaveChangesAsync();
            }
        }
    }
}
