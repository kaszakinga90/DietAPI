using Application.Core;
using Application.DTOs.InvitationDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
                try
                {
                    var invitation = await _context.InvitationsDb
                    .SingleOrDefaultAsync(x => x.Id == request.InvitationId, cancellationToken);

                    if (invitation == null)
                    {
                        return Result<InvitationGetDTO>.Failure("Nie znaleziono zaproszenia.");
                    }

                    var invitationDTO = _mapper.Map<InvitationGetDTO>(invitation);

                    return Result<InvitationGetDTO>.Success(invitationDTO);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<InvitationGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}