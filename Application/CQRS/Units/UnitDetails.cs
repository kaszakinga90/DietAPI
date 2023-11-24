using DietDB;
using MediatR;
using ModelsDB;
namespace Application.CQRS.Units
{
    public class UnitDetails
    {
        public class Query : IRequest<ModelsDB.Functionality.Unit>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ModelsDB.Functionality.Unit>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<ModelsDB.Functionality.Unit> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.UnitsDb.FindAsync(request.Id);
            }
        }
    }
}
