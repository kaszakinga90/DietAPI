using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;

namespace Application.CQRS.Diets
{
    // TODO
    /// <summary>
    /// Zawiera klasy służące do tworzenia nowej w bazie danych.
    /// </summary>
    public class DietCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia nowego dietetyka.
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Pobiera lub ustawia model dietetyka, który ma zostać dodany do bazy danych.
            /// </summary>
            public DietDTO DietDTO { get; set; }
        }

        /// <summary>
        /// Obsługuje proces dodawania nowego dietetyka do bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Przetwarza komendę tworzenia nowego dietetyka w bazie danych.
            /// </summary>
            /// <param name="request">Komenda do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var diet = _mapper.Map<Diet>(request.DietDTO);

                _context.DietsDb.Add(diet);
                await _context.SaveChangesAsync();

                if (diet.MealTimesToXYAxis != null && diet.MealTimesToXYAxis.Any())
                {
                    foreach (var mealTime in diet.MealTimesToXYAxis)
                    {
                        if (mealTime.Id == 0) // Tylko jeśli Id nie jest ustawione
                        {
                            mealTime.DietId = diet.Id;
                            _context.MealTimesDb.Add(mealTime);
                        }
                    }

                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
