using Application.Core;
using Application.DTOs.InvitationDTO;
using AutoMapper;
using DietDB;
using MediatR;

namespace Application.CQRS.Invitations
{
    public class InvitationDetails
    {
        public class Query : IRequest<Result<InvitationGetDTO>>
        {
            public int InvitationId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<InvitationGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<InvitationGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var invitation = await _context.InvitationsDb.FindAsync(request.InvitationId);

                if (invitation == null)
                {
                    return Result<InvitationGetDTO>.Failure("Invitation not found.");
                }

                var invitationDTO = _mapper.Map<InvitationGetDTO>(invitation);

                return Result<InvitationGetDTO>.Success(invitationDTO);
            }
        }
    }
}
