﻿using Application.Core;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Admins
{
    /// <summary>
    /// Zawiera klasy do tworzenia wiadomości skierowanych do pacjenta od admina.
    /// </summary>
    public class MessageToPatientFromAdminCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia wiadomości dla pacjenta.
        /// </summary>
        public class Command : IRequest<Result<MessageToDTO>>
        {
            /// <summary>
            /// Pobiera lub ustawia DTO wiadomości skierowanej do pacjenta.
            /// </summary>
            public MessageToDTO MessageDTO { get; set; }
            public int AdminId { get; set; }
        }

        /// <summary>
        /// Obsługuje proces tworzenia wiadomości dla pacjenta.
        /// </summary>
        public class Handler : IRequestHandler<Command, Result<MessageToDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem i maperem.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi wiadomości dla pacjentów.</param>
            /// <param name="mapper">Obiekt służący do mapowania obiektów.</param>
            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Przetwarza komendę tworzenia wiadomości dla pacjenta.
            /// </summary>
            /// <param name="request">Komenda do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            public async Task<Result<MessageToDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!request.MessageDTO.PatientId.HasValue)
                {
                    return Result<MessageToDTO>.Failure("ID pacjenta jest wymagane.");
                }

                var message = _mapper.Map<MessageTo>(request.MessageDTO);

                message.AdminId = request.AdminId;
                message.DieticianId = null;

                var patient = await _context.PatientsDb
                    .FindAsync(request.MessageDTO.PatientId.Value, cancellationToken);

                if (patient == null)
                {
                    return Result<MessageToDTO>.Failure("Pacjent nie został znaleziony.");
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