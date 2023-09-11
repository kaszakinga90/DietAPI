using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.SingleDiets
{
    public class SingleDietDetails
    {
        public class Query : IRequest<SingleDiet>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, SingleDiet>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<SingleDiet> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.SingleDiet.FindAsync(request.Id);
            }
        }
    }
}
