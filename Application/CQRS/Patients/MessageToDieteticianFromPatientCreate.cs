using Application.Core;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Patients
{
    /// <summary>
    /// Zawiera klasy do tworzenia wiadomości skierowanych do dietetyka.
    /// </summary>
    public class MessageToDieteticianFromPatientCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia wiadomości dla dietetyka.
        /// </summary>
        public class Command : IRequest<Result<MessageToDTO>>
        {
            /// <summary>
            /// Pobiera lub ustawia DTO wiadomości skierowanej do dietetyka.
            /// </summary>
            public MessageToDTO MessageDTO { get; set; }
            public int PatientId { get; set; }
        }

        /// <summary>
        /// Obsługuje proces tworzenia wiadomości dla dietetyka.
        /// </summary>
        public class Handler : IRequestHandler<Command, Result<MessageToDTO>>
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
            public async Task<Result<MessageToDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!request.MessageDTO.DieticianId.HasValue)
                {
                    return Result<MessageToDTO>.Failure("ID dietetyka jest wymagane.");
                }

                var message = _mapper.Map<MessageTo>(request.MessageDTO);

                message.PatientId = request.PatientId;
                message.AdminId = null;

                var dietetician = await _context.DieticiansDb.FindAsync(request.MessageDTO.DieticianId.Value);
                if (dietetician == null)
                {
                    return Result<MessageToDTO>.Failure("Dietetyk nie został znaleziony.");
                }

                _context.MessageToDb.Add(message);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<MessageToDTO>.Failure("Operacja nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<MessageToDTO>.Failure("Wystąpił błąd podczas wysyłania wiadomości.");
                }

                return Result<MessageToDTO>.Success(_mapper.Map<MessageToDTO>(message));
            }
        }
    }
}
