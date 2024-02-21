using Application.Core;
using Application.Validators.Messages;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Dieticians
{
    public class MessageToAdminFromDieticianCreate
    {
        public class Command : IRequest<Result<MessageToDTO>>
        {
            public MessageToDTO MessageDTO { get; set; }
            public int DieticianId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MessageToDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly MessageCreateValidator _validator;

            public Handler(DietContext context, IMapper mapper, MessageCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<MessageToDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.MessageDTO);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<MessageToDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                if (!request.MessageDTO.AdminId.HasValue)
                {
                    return Result<MessageToDTO>.Failure("ID admina jest wymagane.");
                }

                var message = _mapper.Map<MessageTo>(request.MessageDTO);

                message.DieticianId = request.DieticianId;
                message.PatientId = null;

                var admin = await _context.AdminsDb.FindAsync(request.MessageDTO.AdminId.Value);
                if (admin == null)
                {
                    return Result<MessageToDTO>.Failure("Admin nie został znaleziony.");
                }

                _context.MessageToDb.Add(message);

                try
                {
                    var result = await _context.SaveChangesAsync() > 0;
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