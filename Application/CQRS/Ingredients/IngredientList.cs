using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;

namespace Application.CQRS.Ingredients
{
    /// <summary>
    /// Zawiera klasy służące do pobierania listy produktów(składników) z bazy danych.
    /// </summary>
    public class IngredientList
    {
        /// <summary>
        /// Reprezentuje zapytanie do pobrania listy produktów(składników).
        /// </summary>
        public class Query : IRequest<List<Ingredient>> 
        {
            public int? DietitianId { get; }

            public Query(int? dietitianId)
            {
                DietitianId = dietitianId;
            }
        }


        /// <summary>
        /// Obsługuje proces pobierania listy produktów(składników) z bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Query, List<Ingredient>>
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
            /// Przetwarza zapytanie i pobiera listę produktów(składników) z bazy danych.
            /// </summary>
            /// <param name="request">Zapytanie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca listę produktów(składników).</returns>
            public async Task<List<Ingredient>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ingredients = await _context.IngredientsDb
        .Where(i => i.DieticianId == request.DietitianId || (i.DieticianId == null))
        .ToListAsync(cancellationToken);

                return ingredients;
            }
        }
    }
}
