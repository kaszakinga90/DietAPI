using Application.DTOs.IngredientDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ingredients
{
    /// <summary>
    /// Zawiera klasy służące do pobierania szczegółów produktu(składnika) na podstawie jego identyfikatora.
    /// </summary>
    public class IngredientDetails
    {
        /// <summary>
        /// Reprezentuje zapytanie do pobrania szczegółów produktu(składnika) na podstawie identyfikatora.
        /// </summary>
        public class Query : IRequest<IngredientGetDTO>
        {
            /// <summary>
            /// Pobiera lub ustawia identyfikator produktu(składnika), którego szczegóły mają zostać pobrane.
            /// </summary>
            public int Id { get; set; }
        }

        /// <summary>
        /// Obsługuje proces pobierania szczegółów produktu(składnika) z bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Query, IngredientGetDTO>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi produktów(składników).</param>
            /// <param name="mapper">Maper służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Przetwarza zapytanie i zwraca szczegóły produktu(składnika) na podstawie identyfikatora.
            /// </summary>
            /// <param name="request">Zapytanie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca szczegóły produktu(składnika) w postaci obiektu <see cref="IngredientGetDTO"/>.</returns>
            public async Task<IngredientGetDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var ingredient = await _context.IngridientsDb.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return _mapper.Map<IngredientGetDTO>(ingredient);
            }
        }
    }
}
