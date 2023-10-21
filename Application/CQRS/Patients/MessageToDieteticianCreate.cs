using Application.DTOs.MessagesDTO;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Patients
{
    /// <summary>
    /// Zawiera klasy do tworzenia wiadomości skierowanych do dietetyka.
    /// </summary>
    public class MessageToDieteticianCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia wiadomości dla dietetyka.
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Pobiera lub ustawia DTO wiadomości skierowanej do dietetyka.
            /// </summary>
            public MessageToDieteticianDTO MessageDTO { get; set; }
        }

        /// <summary>
        /// Obsługuje proces tworzenia wiadomości dla dietetyka.
        /// </summary>
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi wiadomości dla dietetyków.</param>
            /// <param name="mapper">Obiekt służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Przetwarza komendę tworzenia wiadomości dla dietetyka.
            /// </summary>
            /// <param name="request">Komenda do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var message = _mapper.Map<MessageToDietician>(request.MessageDTO);

                // Przypisuj wiadomość do pacjenta
                var dietetician = await _context.Dieticians.FindAsync(request.MessageDTO.DieticianId);
                if (dietetician == null)
                {
                    // Rzuć błąd jeśli dietetyk nie został znaleziony
                    throw new Exception("Dietetyk nie został znaleziony");
                }

                _context.MessageToDieticians.Add(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
