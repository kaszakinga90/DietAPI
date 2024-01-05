using Application.Core;
using Application.DTOs.DayWeekDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.DayWeeks
{
    public class DayWeekList
    {
        public class Query : IRequest<Result<List<DayWeekGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<DayWeekGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DayWeekGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dayWeeksList = await _context.DayWeeksDb
                    .Select(m => new DayWeekGetDTO
                    {
                        Id = m.Id,
                        Day = m.Day,
                    })
                    .ToListAsync(cancellationToken);

                if (dayWeeksList == null)
                {
                    return Result<List<DayWeekGetDTO>>.Failure("no results.");
                }

                return Result<List<DayWeekGetDTO>>.Success(dayWeeksList);
            }
        }
    }
}