using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using ModelsDB;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.Validators.Diet;
using Application.Core;
using System.Diagnostics;

/// <summary>
/// Klasa służąca do tworzenia nowej diety
/// </summary>
public class DietCreate
{
    // Klasa reprezentująca komendę do stworzenia nowej diety
    public class Command : IRequest<Result<DietPostDTO>>
    {
        public DietPostDTO DietPostDTO { get; set; }
    }

    /// <summary>
    /// Handler obsługujący logikę tworzenia nowej diety
    /// </summary>
    public class Handler : IRequestHandler<Command, Result<DietPostDTO>>
    {
        private readonly DietContext _context;
        private readonly IMapper _mapper;
        // Walidator do sprawdzania poprawności danych diety.
        private readonly DietCreateValidator _validator;

        public Handler(DietContext context, IMapper mapper, DietCreateValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Metoda realizująca logikę tworzenia nowej diety
        /// </summary>
        public async Task<Result<DietPostDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            // sprawdzenie poprawności danych, które przyszły w request
            var validationResult = await _validator
                    .ValidateAsync(request.DietPostDTO);

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

                // Zapisanie nowej diety z podstawowymi danymi w bazie danych
                _context.DietsDb.Add(diet);
                await _context.SaveChangesAsync();

                // Przygotowanie harmonogramu dla diety (zapis przypisanych posiłków)
                await AddMealSchedules(diet, mealTimesDtoList, cancellationToken);

                return Result<DietPostDTO>.Success(_mapper.Map<DietPostDTO>(diet));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                return Result<DietPostDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
            }
        }

        /// <summary>
        /// Metoda obsługująca dodanie posiłków do harmonogramu diety
        /// </summary>
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

                            // Utworzenie i dodanie pojedynczego posiłku dla czasu harmonogramu
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
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Nie utworzono mealschedules. Przyczyna niepowodzenia: " + ex);
            }
        }
    }
}
