using DietDB;
using MediatR;
using ModelsDB;

namespace Application.CQRS.Patients
{
    /// <summary>
    /// Zawiera klasy służące do tworzenia nowego pacjenta w bazie danych.
    /// </summary>
    public class PatientCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia nowego pacjenta.
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Pobiera lub ustawia model pacjenta, który ma zostać dodany do bazy danych.
            /// </summary>
            public Patient Patient { get; set; }
        }

        /// <summary>
        /// Obsługuje proces dodawania nowego pacjenta do bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Command>
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
            /// Przetwarza komendę tworzenia nowego pacjenta w bazie danych.
            /// </summary>
            /// <param name="request">Komenda do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.PatientsDb.Add(request.Patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
