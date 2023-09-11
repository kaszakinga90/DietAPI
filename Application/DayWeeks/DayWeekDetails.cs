using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.DayWeeks
{
    public class DayWeekDetails
    {
        public class Query : IRequest<DayWeek>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, DayWeek>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<DayWeek> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.DayWeek.FindAsync(request.Id);
            }
        }
    }
}
