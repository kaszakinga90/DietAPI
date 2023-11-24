using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Units
{
    public class UnitList
    {
        public class Query : IRequest<List<ModelsDB.Functionality.Unit>> { }

        public class Handler : IRequestHandler<Query, List<ModelsDB.Functionality.Unit>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<ModelsDB.Functionality.Unit>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.UnitsDb.ToListAsync(cancellationToken);
            }
        }
    }
}
