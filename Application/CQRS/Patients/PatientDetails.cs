using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using ModelsDB.Functionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.PatientDTOs
{
    /// <summary>
    /// Zawiera klasy służące do pobierania szczegółów pacjenta na podstawie jego identyfikatora.
    /// </summary>
    public class PatientDetails
    {
        /// <summary>
        /// Reprezentuje zapytanie do pobrania szczegółów pacjenta na podstawie identyfikatora.
        /// </summary>
        public class Query : IRequest<PatientDTO>
        {
            /// <summary>
            /// Pobiera lub ustawia identyfikator pacjenta, którego szczegóły mają zostać pobrane.
            /// </summary>
            public int Id { get; set; }
        }

        /// <summary>
        /// Obsługuje proces pobierania szczegółów pacjenta z bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Query, PatientDTO>
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
            /// Przetwarza zapytanie i zwraca szczegóły pacjenta na podstawie identyfikatora.
            /// </summary>
            /// <param name="request">Zapytanie do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            /// <returns>Zwraca szczegóły pacjenta w postaci obiektu <see cref="PatientDTO"/>.</returns>
            public async Task<PatientDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var patient = await _context.Patients
                                                .Include(p => p.Address)
                                                .Include(p => p.MessageToPatients)
                                                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return _mapper.Map<PatientDTO>(patient);
            }
        }
    }
}
