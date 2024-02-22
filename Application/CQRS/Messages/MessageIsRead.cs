using Application.Core;
using Application.DTOs.MessagesDTO;
using AutoMapper;
using DietDB;
using MediatR;

namespace Application.CQRS.Messages
{
    public class MessageIsRead
    {
        public class Command : IRequest<Result<MessageIsReadPostDTO>>
        {
            public MessageIsReadPostDTO MessageIsReadPostDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MessageIsReadPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<MessageIsReadPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var message = await _context.MessageToDb.FindAsync(request.MessageIsReadPostDTO.Id);

                if (message == null)
                {
                    return Result<MessageIsReadPostDTO>.Failure("Wiadomość nie została znaleziona.");
                }

                message.IsRead = true;

                _context.MessageToDb.Update(message);
                await _context.SaveChangesAsync();

                var updatedMessageDto = _mapper.Map<MessageIsReadPostDTO>(message);

                return Result<MessageIsReadPostDTO>.Success(updatedMessageDto);
            }
        }
    }
}