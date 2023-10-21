using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.MealTimes
{
    public class MealTimeDetails
    {
        public class Query : IRequest<MealTime>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, MealTime>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<MealTime> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.MealTime.FindAsync(request.Id);
            }
        }
    }
}
