using Application.DTOs.AdminDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Admins
{
    /// <summary>
    /// Zawiera klasy służące do pobierania szczegółów adminów na podstawie jego identyfikatora.
    /// </summary>
    public class AdminDetails
    {
        /// <summary>
        /// Reprezentuje zapytanie do pobrania szczegółów admina na podstawie identyfikatora.
        /// </summary>
        public class Query : IRequest<AdminDTO>
        {
            /// <summary>
            /// Pobiera lub ustawia identyfikator admina, którego szczegóły mają zostać pobrane.
            /// </summary>
            public int Id { get; set; }
        }

        /// <summary>
        /// Obsługuje proces pobierania szczegółów admina z bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Query, AdminDTO>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi adminów.</param>
            /// <param name="mapper">Maper służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Przetwarza zapytanie i zwraca szczegóły admina na podstawie identyfikatora.
            /// </summary>
            /// <param name="request">Zapytanie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca szczegóły admina w postaci obiektu <see cref="AdminDTO"/>.</returns>
            public async Task<AdminDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var admin = await _context.AdminsDb
                                                .Include(a => a.Address)
                                                .Include(a => a.MessageTo)
                                                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return _mapper.Map<AdminDTO>(admin);
            }
        }
    }
}
