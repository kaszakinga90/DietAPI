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
    public class LayoutCategoryDetails
    {
        public class Query : IRequest<LayoutCategory>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, LayoutCategory>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<LayoutCategory> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.LayoutCategories.FindAsync(request.Id);
            }
        }
    }
}
