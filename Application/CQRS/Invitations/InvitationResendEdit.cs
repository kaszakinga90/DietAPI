using Application.Core;
using Application.DTOs.InvitationDTO;
using Application.Validators.Invitation;
using AutoMapper;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Invitations
{
    public class InvitationResendEdit
    {
        public class Command : IRequest<Result<InvitationPutDTO>>
        {
            public InvitationPutDTO InvitationPutDTO { get; set; }
            public int InvitationId { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<InvitationPutDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly InvitationUpdateValidator _validator;

            public Handler(DietContext context, IMapper mapper, InvitationUpdateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<InvitationPutDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.InvitationPutDTO);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<InvitationPutDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var invitation = await _context.InvitationsDb
                    .FindAsync(request.InvitationPutDTO.Id);

                if (invitation == null)
                {
                    return Result<InvitationPutDTO>.Failure("Zaproszenie o podanym ID nie zostało znalezione.");
                }

                _mapper.Map(request.InvitationPutDTO, invitation);

                invitation.IsSended = true;
                invitation.IsDeclined = false;

                try
                {
                    var result = await _context.SaveChangesAsync() > 0;
                    if (!result)
                    {
                        return Result<InvitationPutDTO>.Failure("Edycja zaproszenia nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<InvitationPutDTO>.Failure("Wystąpił błąd podczas edycji zaproszenia. " + ex);
                }

                return Result<InvitationPutDTO>.Success(_mapper.Map<InvitationPutDTO>(invitation));
            }
        }
    }
}