using Application.DTOs.MealTimeToXYAxisDTO;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Diets
{
    public class DietCreate
    {
        public class Command : IRequest
        {
            public DietDTO DietDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var diet = _mapper.Map<Diet>(request.DietDTO);

                _context.DietsDb.Add(diet);
                await _context.SaveChangesAsync(cancellationToken);

                if (diet.MealTimesToXYAxis != null && diet.MealTimesToXYAxis.Any())
                {
                    foreach (var mealTime in diet.MealTimesToXYAxis)
                    {
                        if (mealTime.Id == 0)
                        {
                            mealTime.DietId = diet.Id;
                            _context.MealTimesDb.Add(mealTime);
                        }
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }

                await AddMealSchedules(diet, request.DietDTO.MealTimesToXYAxisDTO, cancellationToken);

            }

            private async Task AddMealSchedules(Diet diet, List<MealTimeToXYAxisDTO> mealTimesDto, CancellationToken cancellationToken)
            {
                var startDate = diet.StartDate;
                var endDate = diet.EndDate;

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    foreach (var mealTimeDto in mealTimesDto)
                    {
                        var mealTime = TimeSpan.Parse(mealTimeDto.MealTime);
                        var dateTime = new DateTime(date.Year, date.Month, date.Day, mealTime.Hours, mealTime.Minutes, mealTime.Seconds);

                        var mealSchedule = new MealSchedule
                        {
                            DietId = diet.Id,
                            DishId = 4,
                            MealTime = dateTime
                        };

                        _context.MealSchedulesDb.Add(mealSchedule);
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
