using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.MealTimes
{
    public class MealTimeDetails
    {
        public class Query : IRequest<MealTimeToXYAxis>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, MealTimeToXYAxis>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<MealTimeToXYAxis> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.MealTimesDb.FindAsync(request.Id);
            }
        }
    }
}
