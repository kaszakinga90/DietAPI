using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;

namespace Application.CQRS.Patients
{
    /// <summary>
    /// Zawiera klasy służące do pobierania listy pacjentów z bazy danych.
    /// </summary>
    public class PatientList
    {
        /// <summary>
        /// Reprezentuje zapytanie do pobrania listy pacjentów.
        /// </summary>
        public class Query : IRequest<List<Patient>> { }

        /// <summary>
        /// Obsługuje proces pobierania listy pacjentów z bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Query, List<Patient>>
        {
            private readonly DietContext _context;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi pacjentów.</param>
            public Handler(DietContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Przetwarza zapytanie i pobiera listę pacjentów z bazy danych wraz z ich adresami.
            /// </summary>
            /// <param name="request">Zapytanie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca listę pacjentów.</returns>
            public async Task<List<Patient>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Patients
                    .Include(p => p.Address)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
