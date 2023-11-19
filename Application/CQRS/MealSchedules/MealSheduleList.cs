using Application.Core;
using Application.DTOs.MealScheduleDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.MealSchedules
{
    public class MealSheduleList
    {
        public class Query : IRequest<Result<List<MealScheduleEditDTO>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<MealScheduleEditDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<MealScheduleEditDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var mealSchedule = await _context.MealSchedulesDb
                    .Select(d => new MealScheduleEditDTO
                    {
                        Id = d.Id,
                        DietId = d.DietId,
                        DishId = d.DietId,
                        MealTime = d.MealTime,
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<MealScheduleEditDTO>>.Success(mealSchedule);
            }
        }
    }
}
