using Application.Core;
using Application.DTOs.AdminDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Admins
{
    /// <summary>
    /// Zawiera klasy służące do edycji informacji o adminie.
    /// </summary>
    public class AdminEdit
    {
        /// <summary>
        /// Reprezentuje polecenie do edycji informacji o adminie.
        /// </summary>
        public class Command : IRequest<Result<AdminDTO>>
        {
            /// <summary>
            /// Pobiera lub ustawia informacje o adminie do edycji.
            /// </summary>
            public AdminDTO Admin { get; set; }
            public IFormFile File { get; set; }
        }

        /// <summary>
        /// Walidator do sprawdzania poprawności danych admina przed ich edycją.
        /// </summary>
        public class CommandValidator : AbstractValidator<AdminDTO>
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
        /// Obsługuje proces edycji informacji o adminie w bazie danych.
        /// </summary>
        public class Handler : IRequestHandler<Command, Result<AdminDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi adminów.</param>
            /// <param name="mapper">Maper służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper, ImageService imageService)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
            }

            /// <summary>
            /// Przetwarza polecenie edycji informacji o adminie i zapisuje zmiany w bazie danych.
            /// </summary>
            /// <param name="request">Polecenie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca wynik operacji edycji w postaci obiektu <see cref="AdminDTO"/>.</returns>
            public async Task<Result<AdminDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var admin = await _context.AdminsDb.FindAsync(new object[] { request.Admin.Id }, cancellationToken);
                if (admin == null)
                {
                    return Result<AdminDTO>.Failure("Admin o podanym ID nie został znaleziony.");
                }

                _mapper.Map(request.Admin, admin);

                // Obsługa obrazu
                if (request.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.File);
                    if (imageResult.Error != null)
                    {
                        return Result<AdminDTO>.Failure(imageResult.Error.Message);
                    }

                    if (!string.IsNullOrEmpty(admin.PublicId))
                    {
                        await _imageService.DeleteImageAsync(admin.PublicId);
                    }

                    admin.PictureUrl = imageResult.SecureUrl.ToString();
                    admin.PublicId = imageResult.PublicId;
                }

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<AdminDTO>.Failure("Edycja admina nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    // TODO : Debug.Log zamiast exceptiona
                    return Result<AdminDTO>.Failure("Wystąpił błąd podczas edycji admina.");
                }
                return Result<AdminDTO>.Success(_mapper.Map<AdminDTO>(admin));
            }
        }
    }
}
