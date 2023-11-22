using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.MealTimes
{
    public class MealTimeCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia nowego MealTime.
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Pobiera lub ustawia model MealTime, który ma zostać dodany do bazy danych.
            /// </summary>
            public MealTimeToXYAxis MealTime { get; set; }
        }

        /// <summary>
        /// Obsługuje proces dodawania nowego MealTime do bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi MealTimes.</param>
            public Handler(DietContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Przetwarza komendę tworzenia nowego MealTime w bazie danych.
            /// </summary>
            /// <param name="request">Komenda do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.MealTimesDb.Add(request.MealTime);
                await _context.SaveChangesAsync();
            }
        }
    }
}
