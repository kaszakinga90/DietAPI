using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.Admins
{
    /// <summary>
    /// Zawiera klasy do tworzenia wiadomości skierowanych do dietetyka od admina.
    /// </summary>
    public class MessageToDieteticianFromAdminCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia wiadomości dla dietetyka.
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Pobiera lub ustawia DTO wiadomości skierowanej do dietetyka.
            /// </summary>
            public MessageToDTO MessageDTO { get; set; }
            public int AdminId { get; set; }
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
                if (!request.MessageDTO.AdminId.HasValue)
                {
                    throw new ArgumentException("ID dietetyka jest wymagane");
                }

                var message = _mapper.Map<MessageTo>(request.MessageDTO);

                // Ustawienie ID admina dla wiadomości
                message.AdminId = request.AdminId;
                message.PatientId = null;

                // Przypisuj wiadomość do dietetyka
                var dietetician = await _context.DieticiansDb.FindAsync(request.MessageDTO.DieticianId.Value);
                if (dietetician == null)
                {
                    throw new Exception("Dietetyk nie został znaleziony");
                }

                _context.MessageToDb.Add(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
