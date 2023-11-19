using DietDB;
using MediatR;
using ModelsDB;

namespace Application.CQRS.Ingredients
{
    /// <summary>
    /// Zawiera klasy służące do tworzenia nowego produktu(składnika) w bazie danych.
    /// </summary>
    public class IngredientCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia nowego produktu(składnika).
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Pobiera lub ustawia model produktu(składnika), który ma zostać dodany do bazy danych.
            /// </summary>
            public Ingredient Ingredient { get; set; }
        }

        /// <summary>
        /// Obsługuje proces dodawania nowego produktu(składnika) do bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi produktów(składników).</param>
            public Handler(DietContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Przetwarza komendę tworzenia nowego produktu(składnika) w bazie danych.
            /// </summary>
            /// <param name="request">Komenda do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.IngridientsDb.Add(request.Ingredient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
