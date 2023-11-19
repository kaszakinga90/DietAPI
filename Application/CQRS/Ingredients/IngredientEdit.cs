using Application.Core;
using Application.DTOs.IngredientDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Ingredients
{
    /// <summary>
    /// Zawiera klasy służące do edycji informacji o produkcie(składniku).
    /// </summary>
    public class IngredientEdit
    {
        /// <summary>
        /// Reprezentuje polecenie do edycji informacji o produkcie(składniku).
        /// </summary>
        public class Command : IRequest<Result<IngredientDTO>>
        {
            /// <summary>
            /// Pobiera lub ustawia informacje o produkcie(składniku) do edycji.
            /// </summary>
            public IngredientDTO Ingredient { get; set; }
            public IFormFile File { get; set; }
        }

        /// <summary>
        /// Walidator do sprawdzania poprawności danych produktu(składnika) przed ich edycją.
        /// </summary>
        public class CommandValidator : AbstractValidator<IngredientDTO>
        {
            /// <summary>
            /// Inicjalizuje walidator i definiuje reguły walidacji.
            /// </summary>
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Nazwa wymagana");
            }
        }

        /// <summary>
        /// Obsługuje proces edycji informacji o produkcie(składniku) w bazie danych.
        /// </summary>
        public class Handler : IRequestHandler<Command, Result<IngredientDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi produktów(składników).</param>
            /// <param name="mapper">Maper służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper, ImageService imageService)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
            }

            /// <summary>
            /// Przetwarza polecenie edycji informacji o produkcie(składniku) i zapisuje zmiany w bazie danych.
            /// </summary>
            /// <param name="request">Polecenie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca wynik operacji edycji w postaci obiektu <see cref="IngredientDTO"/>.</returns>
            public async Task<Result<IngredientDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ingredient = await _context.IngridientsDb.FindAsync(new object[] { request.Ingredient.Id }, cancellationToken);
                if (ingredient == null)
                {
                    return Result<IngredientDTO>.Failure("Produkt o podanym ID nie został znaleziony.");
                }

                _mapper.Map(request.Ingredient, ingredient);

                // Obsługa obrazu
                if (request.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.File);
                    if (imageResult.Error != null)
                    {
                        // Logowanie błędu i zwrócenie informacji o błędzie
                        return Result<IngredientDTO>.Failure(imageResult.Error.Message);
                    }

                    if (!string.IsNullOrEmpty(ingredient.PublicId))
                    {
                        await _imageService.DeleteImageAsync(ingredient.PublicId);
                    }

                    ingredient.PictureUrl = imageResult.SecureUrl.ToString();
                    ingredient.PublicId = imageResult.PublicId;
                }

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<IngredientDTO>.Failure("Edycja produktu nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    return Result<IngredientDTO>.Failure("Wystąpił błąd podczas edycji produktu.");
                }

                return Result<IngredientDTO>.Success(_mapper.Map<IngredientDTO>(ingredient));
            }
        }
    }
}
