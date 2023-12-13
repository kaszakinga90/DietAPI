using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.Dieticians
{
    /// <summary>
    /// Zawiera klasy do tworzenia wiadomości skierowanych do pacjenta od dietetyka.
    /// </summary>
    public class MessageToPatientFromDieticianCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia wiadomości dla dietetyka.
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Pobiera lub ustawia DTO wiadomości skierowanej do pacjenta.
            /// </summary>
            public MessageToDTO MessageDTO { get; set; }
            public int DieticianId { get; set; }
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
                if (!request.MessageDTO.PatientId.HasValue)
                {
                    throw new ArgumentException("ID pacjenta jest wymagane");
                }

                var message = _mapper.Map<MessageTo>(request.MessageDTO);

                message.DieticianId = request.DieticianId;
                message.AdminId = null;

                var patient = await _context.PatientsDb.FindAsync(request.MessageDTO.PatientId.Value);
                if (patient == null)
                {
                    throw new Exception("Pacjent nie został znaleziony");
                }

                _context.MessageToDb.Add(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
