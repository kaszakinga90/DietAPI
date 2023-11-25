using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using ModelsDB;
using Application.DTOs.MealTimeToXYAxisDTO;
using Microsoft.EntityFrameworkCore;

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
            var mealTimesDtoList = request.DietDTO.MealTimesToXYAxisDTO;

            request.DietDTO.MealTimesToXYAxisDTO = null;
            var diet = _mapper.Map<Diet>(request.DietDTO);

            _context.DietsDb.Add(diet);
            await _context.SaveChangesAsync(cancellationToken);

            await AddMealSchedules(diet, mealTimesDtoList, cancellationToken);
        }

        private async Task AddMealSchedules(Diet diet, List<MealTimeToXYAxisDTO> mealTimesDto, CancellationToken cancellationToken)
        {
            var startDate = diet.StartDate;
            var endDate = diet.EndDate;
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Assuming MealTime is a string in "HH:mm:ss" format
                //var dateTime = new DateTime(diet.StartDate.Year, diet.StartDate.Month, diet.StartDate.Day, mealTime.Hours, mealTime.Minutes, mealTime.Seconds);

                // Assuming you need to create a record for each day of the diet
                foreach (var mealTimeDto in mealTimesDto)
                {
                    //var mealTime = mealTimeDto.MealTime.TimeOfDay;
                    var mealTime = TimeSpan.Parse(mealTimeDto.MealTime);

                    var mealDateTime = new DateTime(date.Year, date.Month, date.Day, mealTime.Hours, mealTime.Minutes, mealTime.Seconds);

                    var mealSchedule = new MealTimeToXYAxis
                    {
                        DietId = diet.Id,
                        MealId = mealTimeDto.MealId,
                        MealTime = mealDateTime,
                        DishId=null
                    };

                    Console.WriteLine(" ==================================");
                    Console.WriteLine(" Zapisuje do bazy: " + mealSchedule.DietId + " " + mealSchedule.MealTime);
                    Console.WriteLine(" ==================================");
                    _context.MealTimesDb.Add(mealSchedule);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}