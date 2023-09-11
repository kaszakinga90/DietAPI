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
    public class MainNavbarDetails
    {
        public class Query : IRequest<MainNavbar>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, MainNavbar>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<MainNavbar> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.MainNavbars.FindAsync(request.Id);
            }
        }
    }
}
