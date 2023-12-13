// TODO : whole class
using Application.DTOs.DieticianDTO;
using AutoMapper;
using DietDB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Diets
{
    // TODO
    /// <summary>
    /// Zawiera klasy służące do pobierania szczegółów dietetyka na podstawie jego identyfikatora.
    /// </summary>
    public class DietDetails
    {
        /// <summary>
        /// Reprezentuje zapytanie do pobrania szczegółów dietetyka na podstawie identyfikatora.
        /// </summary>
        //public class Query : IRequest<DietGetDTO>
        //{
        //    /// <summary>
        //    /// Pobiera lub ustawia identyfikator dietetyka, którego szczegóły mają zostać pobrane.
        //    /// </summary>
        //    public int Id { get; set; }
        //}

        ///// <summary>
        ///// Obsługuje proces pobierania szczegółów dietetyka z bazy danych.
        ///// </summary>
        //public class Handler : IRequestHandler<Query, DietGetDTO>
        //{
        //    private readonly DietContext _context;
        //    private readonly IMapper _mapper;

        //    /// <summary>
        //    /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych i maperem.
        //    /// </summary>
        //    /// <param name="context">Kontekst bazy danych do obsługi dietetyków.</param>
        //    /// <param name="mapper">Maper służący do mapowania obiektów.</param>
        //    public Handler(DietContext context, IMapper mapper)
        //    {
        //        _context = context;
        //        _mapper = mapper;
        //    }

        //    /// <summary>
        //    /// Przetwarza zapytanie i zwraca szczegóły dietetyka na podstawie identyfikatora.
        //    /// </summary>
        //    /// <param name="request">Zapytanie do przetworzenia.</param>
        //    /// <param name="cancellationToken">Token anulowania operacji.</param>
        //    /// <returns>Zwraca szczegóły dietetyka w postaci obiektu <see cref="DietGetDTO"/>.</returns>
        //    public async Task<DietGetDTO> Handle(Query request, CancellationToken cancellationToken)
        //    {
        //        var diet = await _context.DietsDb.

        //                                        .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        //        return _mapper.Map<DietGetDTO>(diet);
        //    }
        //}
    }
}
