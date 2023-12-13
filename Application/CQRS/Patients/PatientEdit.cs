using Application.Core;
using Application.DTOs.PatientDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

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
        public class Command : IRequest<Result<PatientDTO>>
        {
            /// <summary>
            /// Pobiera lub ustawia informacje o pacjencie do edycji.
            /// </summary>
            public PatientDTO Patient { get; set; }
            public IFormFile File { get; set; }
        }

        /// <summary>
        /// Walidator do sprawdzania poprawności danych pacjenta przed ich edycją.
        /// </summary>
        public class CommandValidator : AbstractValidator<PatientDTO>
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
        public class Handler : IRequestHandler<Command, Result<PatientDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi pacjentów.</param>
            /// <param name="mapper">Maper służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper, ImageService imageService)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
            }

            /// <summary>
            /// Przetwarza polecenie edycji informacji o pacjencie i zapisuje zmiany w bazie danych.
            /// </summary>
            /// <param name="request">Polecenie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca wynik operacji edycji w postaci obiektu <see cref="PatientDTO"/>.</returns>
            public async Task<Result<PatientDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var patient = await _context.PatientsDb.FindAsync(new object[] { request.Patient.Id }, cancellationToken);
                if (patient == null)
                {
                    return Result<PatientDTO>.Failure("Pacjent o podanym ID nie został znaleziony.");
                }

                _mapper.Map(request.Patient, patient);

                // Obsługa obrazu
                if (request.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.File);
                    if (imageResult.Error != null)
                    {
                        return Result<PatientDTO>.Failure(imageResult.Error.Message);
                    }

                    if (!string.IsNullOrEmpty(patient.PublicId))
                    {
                        await _imageService.DeleteImageAsync(patient.PublicId);
                    }

                    patient.PictureUrl = imageResult.SecureUrl.ToString();
                    patient.PublicId = imageResult.PublicId;
                }

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<PatientDTO>.Failure("Edycja pacjenta nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    return Result<PatientDTO>.Failure("Wystąpił błąd podczas edycji pacjenta.");
                }

                return Result<PatientDTO>.Success(_mapper.Map<PatientDTO>(patient));
            }
        }
    }
}
