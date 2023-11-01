using Application.Core;
using DietDB;
using MediatR;
using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public class Command : IRequest<PatientUpdateDTO<Unit>>
        {
            /// <summary>
            /// Pobiera lub ustawia model pacjenta, który ma zostać dodany do bazy danych.
            /// </summary>
            public Patient Patient { get; set; }
        }

        /// <summary>
        /// Obsługuje proces dodawania nowego pacjenta do bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Command, PatientUpdateDTO<Unit>>
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
            public async Task<PatientUpdateDTO<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Patients.Add(request.Patient);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return PatientUpdateDTO<Unit>.Failure("Nie udalo sie zapisac example");

                return PatientUpdateDTO<Unit>.Success(Unit.Value);
            }
        }
    }
}
