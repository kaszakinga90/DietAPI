using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LayoutCategories
{
    public class LayoutCategoryList
    {
        public class Query : IRequest<List<LayoutCategory>> { }

        public class Handler : IRequestHandler<Query, List<LayoutCategory>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<LayoutCategory>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.LayoutCategories.ToListAsync(cancellationToken);
            }
        }
    }
}
