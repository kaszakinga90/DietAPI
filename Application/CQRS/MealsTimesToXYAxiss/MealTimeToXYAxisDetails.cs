using Application.Core;
using Application.DTOs.MealTimeToXYAxisDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.MealsTimesToXYAxiss
{
    public class MealTimeToXYAxisDetails
    {
        public class Query : IRequest<Result<List<MealTimeToXYAxisGetDTO>>>
        {
            public int DietId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<MealTimeToXYAxisGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<MealTimeToXYAxisGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var mealTimeToXYAxis = await _context.MealTimesDb
                    .Where(d => d.DietId == request.DietId)
                    .Select(d => new MealTimeToXYAxisGetDTO
                    {
                        Id = d.Id,
                        DietId = d.DietId,
                        DishId = d.DishId,
                        MealTime = d.MealTime,
                        DishName = d.Dish.Name,
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<MealTimeToXYAxisGetDTO>>.Success(mealTimeToXYAxis);
            }
        }
    }
}

