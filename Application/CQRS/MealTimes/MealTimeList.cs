using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Functionality;

namespace Application.CQRS.MealTimes
{
    public class MealTimeList
    {
        public class Query : IRequest<List<MealTimeToXYAxis>> { }

        public class Handler : IRequestHandler<Query, List<MealTimeToXYAxis>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<MealTimeToXYAxis>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.MealTimesDb.ToListAsync(cancellationToken);
            }
        }
    }
}
