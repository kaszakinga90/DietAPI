using Application.Core;
using AutoMapper;
using DietDB;
using FluentValidation;
using MediatR;
using ModelsDB;

namespace Application.CQRS.Patients
{
    /// <summary>
    /// Zawiera klasy służące do edycji informacji o pacjencie.
    /// </summary>
    public class PatientEdit
    {
        /// <summary>
        /// Reprezentuje polecenie do edycji informacji o pacjencie.
        /// </summary>
        public class Command : IRequest<PatientUpdateDTO<Unit>>
        {
            /// <summary>
            /// Pobiera lub ustawia informacje o pacjencie do edycji.
            /// </summary>
            public PatientDTO Patient { get; set; }
        }

        /// <summary>
        /// Walidator do sprawdzania poprawności danych pacjenta przed ich edycją.
        /// </summary>
        public class CommandValidator : AbstractValidator<PatientUpdateDTO>
        {
            /// <summary>
            /// Inicjalizuje walidator i definiuje reguły walidacji.
            /// </summary>
            public CommandValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty().WithMessage("Imie wymagane");
            }
        }

        /// <summary>
        /// Obsługuje proces edycji informacji o pacjencie w bazie danych.
        /// </summary>
        public class Handler : IRequestHandler<Command, PatientUpdateDTO<Unit>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi pacjentów.</param>
            /// <param name="mapper">Maper służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Przetwarza polecenie edycji informacji o pacjencie i zapisuje zmiany w bazie danych.
            /// </summary>
            /// <param name="request">Polecenie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca wynik operacji edycji w postaci obiektu <see cref="PatientUpdateDTO{Unit}"/>.</returns>
            public async Task<PatientUpdateDTO<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var patient = await _context.Patients.FindAsync(request.Patient.Id);
                if (patient == null) return null;
                _mapper.Map(request.Patient, patient);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return PatientUpdateDTO<Unit>.Failure("Edycja pacjent nie powiodla sie");

                return PatientUpdateDTO<Unit>.Success(Unit.Value);
            }
        }
    }
}
