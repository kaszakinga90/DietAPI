using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using ModelsDB;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.Validators.Diet;

// TODO : sygnatura Handle do przerobienia tak, żeby można było dać walidację
public class DietCreate
{
    public class Command : IRequest
    {
        public DietPostDTO DietPostDTO { get; set; }
    }

    // może dodać result do obsługi poniżej w metodzie Handle
    public class Handler : IRequestHandler<Command>
    {
        private readonly DietContext _context;
        private readonly IMapper _mapper;
        private readonly DietCreateValidator _validator;

        public Handler(DietContext context, IMapper mapper, DietCreateValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            //var validationResult = await _validator
            //        .ValidateAsync(request.DietPostDTO, cancellationToken);

            //if (!validationResult.IsValid)
            //{
            //    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
            //    return Result<DietPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
            //}

            var mealTimesDtoList = request.DietPostDTO.MealTimesToXYAxisDTO;

            request.DietPostDTO.MealTimesToXYAxisDTO = null;
            var diet = _mapper.Map<Diet>(request.DietPostDTO);

            _context.DietsDb.Add(diet);
            await _context.SaveChangesAsync(cancellationToken);

            await AddMealSchedules(diet, mealTimesDtoList, cancellationToken);
        }

        private async Task AddMealSchedules(Diet diet, List<MealTimeToXYAxisPostDTO> mealTimesDto, CancellationToken cancellationToken)
        {
            var startDate = diet.StartDate;
            var endDate = diet.EndDate;

            // TODO : zamknąc poniższe w try catch
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                foreach (var mealTimeDto in mealTimesDto)
                {
                    if (TimeSpan.TryParse(mealTimeDto.MealTime, out TimeSpan time))
                    {
                        var mealDateTime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);

                        var mealSchedule = new MealTimeToXYAxis
                        {
                            DietId = diet.Id,
                            MealId = mealTimeDto.MealId,
                            MealTime = mealDateTime,
                            DishId = null
                        };

                        Console.WriteLine(" ==================================");
                        Console.WriteLine(" Zapisuje do bazy: " + mealSchedule.DietId + " " + mealSchedule.MealTime);
                        Console.WriteLine(" ==================================");
                        _context.MealTimesDb.Add(mealSchedule);
                    }
                    else
                    {
                        // TODO: Obsługa błędu konwersji
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}