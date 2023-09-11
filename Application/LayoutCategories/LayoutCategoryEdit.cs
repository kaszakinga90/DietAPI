using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LayoutCategories
{
    public class LayoutCategoryEdit
    {
        public class Command : IRequest
        {
            public LayoutCategory LayoutCategory { get; set; }
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
                var LayoutCategory = await _context.LayoutCategories.FindAsync(request.LayoutCategory.Id);
                _mapper.Map(request.LayoutCategory, LayoutCategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
