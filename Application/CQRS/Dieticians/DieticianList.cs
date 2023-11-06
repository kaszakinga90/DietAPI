using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;

namespace Application.CQRS.Dieticians
{
    /// <summary>
    /// Kontroler odpowiedzialny za zarządzanie dietetykami.
    /// </summary>
    public class DieticianList
    {
        /// <summary>
        /// Reprezentuje zapytanie do pobrania listy dietetyków.
        /// </summary>
        public class Query : IRequest<List<Dietician>> { }

        /// <summary>
        /// Obsługuje proces pobierania listy dietetyków z bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Query, List<Dietician>>
        {
            private readonly DietContext _context;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi dietetyków.</param>
            public Handler(DietContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Przetwarza zapytanie i pobiera listę dietetyków z bazy danych wraz z ich adresami.
            /// </summary>
            /// <param name="request">Zapytanie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca listę dietetyków.</returns>
            public async Task<List<Dietician>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.DieticiansDb
                    .Include(p => p.Address)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
