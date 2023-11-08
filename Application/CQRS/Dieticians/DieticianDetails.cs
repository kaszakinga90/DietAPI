using Application.DTOs.DieticianDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Dieticians
{
    /// <summary>
    /// Zawiera klasy służące do pobierania szczegółów dietetyka na podstawie jego identyfikatora.
    /// </summary>
    public class DieticianDetails
    {
        /// <summary>
        /// Reprezentuje zapytanie do pobrania szczegółów dietetyka na podstawie identyfikatora.
        /// </summary>
        public class Query : IRequest<DieticianGetDTO>
        {
            /// <summary>
            /// Pobiera lub ustawia identyfikator dietetyka, którego szczegóły mają zostać pobrane.
            /// </summary>
            public int Id { get; set; }
        }

        /// <summary>
        /// Obsługuje proces pobierania szczegółów dietetyka z bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Query, DieticianGetDTO>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi dietetyków.</param>
            /// <param name="mapper">Maper służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Przetwarza zapytanie i zwraca szczegóły dietetyka na podstawie identyfikatora.
            /// </summary>
            /// <param name="request">Zapytanie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca szczegóły dietetyka w postaci obiektu <see cref="DieticianGetDTO"/>.</returns>
            public async Task<DieticianGetDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var dietician = await _context.DieticiansDb
                                                .Include(p => p.Address)
                                                .Include(p => p.MessageTo)
                                                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return _mapper.Map<DieticianGetDTO>(dietician);
            }
        }
    }
}
