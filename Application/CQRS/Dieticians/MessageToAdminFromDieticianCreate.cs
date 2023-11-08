using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.Dieticians
{
    /// <summary>
    /// Zawiera klasy do tworzenia wiadomości skierowanych do admina od dietetyka.
    /// </summary>
    public class MessageToAdminFromDieticianCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia wiadomości dla admina.
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Pobiera lub ustawia DTO wiadomości skierowanej do admina.
            /// </summary>
            public MessageToDTO MessageDTO { get; set; }
            public int DieticianId { get; set; }
        }

        /// <summary>
        /// Obsługuje proces tworzenia wiadomości dla admina.
        /// </summary>
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi wiadomości dla adminów.</param>
            /// <param name="mapper">Obiekt służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Przetwarza komendę tworzenia wiadomości dla admina.
            /// </summary>
            /// <param name="request">Komenda do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                if (!request.MessageDTO.AdminId.HasValue)
                {
                    throw new ArgumentException("ID admina jest wymagane");
                }

                var message = _mapper.Map<MessageTo>(request.MessageDTO);

                // Ustawienie ID dietetyka dla wiadomości
                message.DieticianId = request.DieticianId;
                message.PatientId = null;

                // Przypisuj wiadomość do admina
                var admin = await _context.AdminsDb.FindAsync(request.MessageDTO.AdminId.Value);
                if (admin == null)
                {
                    // Rzuć błąd jeśli admin nie został znaleziony
                    throw new Exception("Admin nie został znaleziony");
                }

                _context.MessageToDb.Add(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
