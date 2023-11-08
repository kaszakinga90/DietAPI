using Application.Core;
using Application.DTOs.DieticianDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Dieticians
{
    /// <summary>
    /// Zawiera klasy służące do edycji informacji o dietetyku.
    /// </summary>
    public class DieticianEdit
    {
        /// <summary>
        /// Reprezentuje polecenie do edycji informacji o dietetyku.
        /// </summary>
        public class Command : IRequest<Result<DieticianDTO>>
        {
            /// <summary>
            /// Pobiera lub ustawia informacje o dietetyku do edycji.
            /// </summary>
            public DieticianDTO Dietician { get; set; }
            public IFormFile File { get; set; }
        }

        /// <summary>
        /// Walidator do sprawdzania poprawności danych dietetyka przed ich edycją.
        /// </summary>
        public class CommandValidator : AbstractValidator<DieticianDTO>
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
        /// Obsługuje proces edycji informacji o dietetyku w bazie danych.
        /// </summary>
        public class Handler : IRequestHandler<Command, Result<DieticianDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi dietetyków.</param>
            /// <param name="mapper">Maper służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper, ImageService imageService)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
            }

            /// <summary>
            /// Przetwarza polecenie edycji informacji o dietetyku i zapisuje zmiany w bazie danych.
            /// </summary>
            /// <param name="request">Polecenie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca wynik operacji edycji w postaci obiektu <see cref="DieticianDTO"/>.</returns>
            public async Task<Result<DieticianDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var dietician = await _context.DieticiansDb.FindAsync(new object[] { request.Dietician.Id }, cancellationToken);
                if (dietician == null)
                {
                    return Result<DieticianDTO>.Failure("Dietetyk o podanym ID nie został znaleziony.");
                }

                _mapper.Map(request.Dietician, dietician);

                // Obsługa obrazu
                if (request.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.File);
                    if (imageResult.Error != null)
                    {
                        // Logowanie błędu i zwrócenie informacji o błędzie
                        return Result<DieticianDTO>.Failure(imageResult.Error.Message);
                    }

                    if (!string.IsNullOrEmpty(dietician.PublicId))
                    {
                        await _imageService.DeleteImageAsync(dietician.PublicId);
                    }

                    dietician.PictureUrl = imageResult.SecureUrl.ToString();
                    dietician.PublicId = imageResult.PublicId;
                }

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<DieticianDTO>.Failure("Edycja dietetyka nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    // Logowanie błędu ex
                    return Result<DieticianDTO>.Failure("Wystąpił błąd podczas edycji dietetyka.");
                }

                //var updatedDietician = _mapper.Map<DieticianDTO>(dietician);
                //return Result<DieticianDTO>.Success(updatedDietician);
                return Result<DieticianDTO>.Success(_mapper.Map<DieticianDTO>(dietician));
            }
        }
    }
}
