using Application.Core;
using Application.DTOs.IngredientDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ModelsDB;

namespace Application.CQRS.Ingredients
{
    /// <summary>
    /// Zawiera klasy służące do tworzenia nowego produktu(składnika) w bazie danych.
    /// </summary>
    public class IngredientCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia nowego produktu(składnika).
        /// </summary>
        public class Command : IRequest<Result<IngredientDTO>>
        {
            /// <summary>
            /// Pobiera lub ustawia model produktu(składnika), który ma zostać dodany do bazy danych.
            /// </summary>
            public IngredientDTO IngredientDTO { get; set; }
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
        /// Obsługuje proces dodawania nowego produktu(składnika) do bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Command, Result<IngredientDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi produktów(składników).</param>
            public Handler(DietContext context, IMapper mapper, ImageService imageService)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
            }

            /// <summary>
            /// Przetwarza komendę tworzenia nowego produktu(składnika) w bazie danych.
            /// </summary>
            /// <param name="request">Komenda do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            public async Task<Result<IngredientDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var newIngredient = _mapper.Map<Ingredient>(request.IngredientDTO);


                if (request.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.File);
                    if (imageResult.Error != null)
                    {
                        // Logowanie błędu i zwrócenie informacji o błędzie
                        return Result<IngredientDTO>.Failure(imageResult.Error.Message);
                    }

                    newIngredient.PictureUrl = imageResult.SecureUrl.ToString();
                    newIngredient.PublicId = imageResult.PublicId;
                }

                try
                {
                    // Dodanie nowego składnika do kontekstu bazy danych
                    _context.IngridientsDb.Add(newIngredient);

                    // Zapisanie zmian w bazie danych
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<IngredientDTO>.Failure("Dodanie nowego produktu nie powiodło się.");
                    }
                }
                catch (Exception ex)
                {
                    return Result<IngredientDTO>.Failure("Wystąpił błąd podczas dodawania nowego produktu.");
                }

                return Result<IngredientDTO>.Success(_mapper.Map<IngredientDTO>(newIngredient));
            }
        }
    }
}
