using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;

namespace Application.CQRS.Admins
{
    /// <summary>
    /// Zawiera klasy służące do pobierania listy adminów z bazy danych.
    /// </summary>
    public class AdminList
    {
        /// <summary>
        /// Reprezentuje zapytanie do pobrania listy adminów.
        /// </summary>
        public class Query : IRequest<List<Admin>> { }

        /// <summary>
        /// Obsługuje proces pobierania listy adminów z bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Query, List<Admin>>
        {
            private readonly DietContext _context;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi adminów.</param>
            public Handler(DietContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Przetwarza zapytanie i pobiera listę adminów z bazy danych wraz z ich adresami.
            /// </summary>
            /// <param name="request">Zapytanie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca listę adminów.</returns>
            public async Task<List<Admin>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.AdminsDb
                    .Include(a => a.Address)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
