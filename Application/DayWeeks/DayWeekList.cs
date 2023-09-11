using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.DayWeeks
{
    public class DayWeekList
    {
        public class Query : IRequest<List<DayWeek>> { }

        public class Handler : IRequestHandler<Query, List<DayWeek>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<DayWeek>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.DayWeek.ToListAsync(cancellationToken);
            }
        }
    }
}