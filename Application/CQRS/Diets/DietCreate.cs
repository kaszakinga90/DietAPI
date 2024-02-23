using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using ModelsDB;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.Validators.Diet;
using Application.Core;
using System.Diagnostics;

// Definicja klasy odpowiedzialnej za tworzenie nowej diety.
public class DietCreate
{
    // Klasa reprezentująca komendę do stworzenia diety.
    public class Command : IRequest<Result<DietPostDTO>>
    {
        public DietPostDTO DietPostDTO { get; set; }
    }

    // Handler obsługujący logikę tworzenia nowej diety.
    public class Handler : IRequestHandler<Command, Result<DietPostDTO>>
    {
        // Kontekst bazy danych dla diet.
        private readonly DietContext _context;
        // Narzędzie do mapowania obiektów DTO na encje bazy danych.
        private readonly IMapper _mapper;
        // Walidator do sprawdzania poprawności danych diety.
        private readonly DietCreateValidator _validator;

        // Konstruktor inicjalizujący handlera.
        public Handler(DietContext context, IMapper mapper, DietCreateValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        // Metoda obsługująca komendę tworzenia diety.
        public async Task<Result<DietPostDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            // Walidacja danych wejściowych.
            var validationResult = await _validator
                    .ValidateAsync(request.DietPostDTO);

            // Jeśli walidacja nie powiedzie się, zwróć błąd z komunikatami.
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                return Result<DietPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
            }

            try
            {
                // Usunięcie powiązania DTO posiłków z DTO diety przed mapowaniem.
                var mealTimesDtoList = request.DietPostDTO.MealTimesToXYAxisDTO;
                request.DietPostDTO.MealTimesToXYAxisDTO = null;
                var diet = _mapper.Map<Diet>(request.DietPostDTO);

                // Dodanie nowej diety do bazy danych.
                _context.DietsDb.Add(diet);
                await _context.SaveChangesAsync();

                // Dodanie harmonogramu posiłków do diety.
                await AddMealSchedules(diet, mealTimesDtoList, cancellationToken);

                // Zwróć wynik zawierający zmapowaną dietę.
                return Result<DietPostDTO>.Success(_mapper.Map<DietPostDTO>(diet));
            }
            catch (Exception ex)
            {
                // W przypadku błędu, zarejestruj go i zwróć informację o błędzie.
                Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                return Result<DietPostDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
            }
        }

        // Metoda prywatna do dodawania harmonogramu posiłków do diety.
        private async Task AddMealSchedules(Diet diet, List<MealTimeToXYAxisPostDTO> mealTimesDto, CancellationToken cancellationToken)
        {
            try
            {
                // Iteracja przez wszystkie dni diety.
                var startDate = diet.StartDate;
                var endDate = diet.EndDate;

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    // Iteracja przez wszystkie posiłki w DTO.
                    foreach (var mealTimeDto in mealTimesDto)
                    {
                        // Próba konwersji czasu posiłku na obiekt TimeSpan.
                        if (TimeSpan.TryParse(mealTimeDto.MealTime, out TimeSpan time))
                        {
                            // Utworzenie daty i czasu posiłku.
                            var mealDateTime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);

                            // Utworzenie i dodanie harmonogramu posiłku.
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
                            // Rejestracja błędu, jeśli konwersja czasu się nie powiedzie.
                            Debug.WriteLine($"Błąd konwersji dla wartości MealTime: {mealTimeDto.MealTime}");
                        }
                    }
                }

                // Zapisanie zmian w bazie danych.
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // W przypadku błędu, zarejestruj go.
                Debug.WriteLine("Nie utworzono mealschedules. Przyczyna niepowodzenia: " + ex);
            }
        }
    }
}
