using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using ModelsDB;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.Validators.Diet;
using Application.Core;
using System.Diagnostics;

public class DietCreate
{
    public class Command : IRequest<Result<DietPostDTO>>
    {
        public DietPostDTO DietPostDTO { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<DietPostDTO>>
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

        public async Task<Result<DietPostDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator
                    .ValidateAsync(request.DietPostDTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                return Result<DietPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
            }

            try
            {
                var mealTimesDtoList = request.DietPostDTO.MealTimesToXYAxisDTO;

                request.DietPostDTO.MealTimesToXYAxisDTO = null;
                var diet = _mapper.Map<Diet>(request.DietPostDTO);

                _context.DietsDb.Add(diet);
                await _context.SaveChangesAsync(cancellationToken);

                await AddMealSchedules(diet, mealTimesDtoList, cancellationToken);

                return Result<DietPostDTO>.Success(_mapper.Map<DietPostDTO>(diet));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                return Result<DietPostDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
            }
        }

        private async Task AddMealSchedules(Diet diet, List<MealTimeToXYAxisPostDTO> mealTimesDto, CancellationToken cancellationToken)
        {
            try
            {
                var startDate = diet.StartDate;
                var endDate = diet.EndDate;

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

                            _context.MealTimesDb.Add(mealSchedule);
                        }
                        else
                        {
                            Debug.WriteLine($"Błąd konwersji dla wartości MealTime: {mealTimeDto.MealTime}");
                        }
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Nie utworzono mealschedules. Przyczyna niepowodzenia: " + ex);
            }
        }
    }
}