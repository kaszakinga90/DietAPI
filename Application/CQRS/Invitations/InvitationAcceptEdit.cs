using Application.Core;
using Application.DTOs.InvitationDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Invitations
{
    public class InvitationAcceptEdit
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

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<InvitationPutDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var invitation = await _context.InvitationsDb
                    .FirstOrDefaultAsync(i => i.Id == request.InvitationId, cancellationToken);

                if (invitation == null)
                {
                    return Result<InvitationPutDTO>.Failure("Zaproszenie o podanym ID nie zostało znalezione.");
                }

                _mapper.Map(request.InvitationPutDTO, invitation);

                invitation.IsAccepted = true;

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
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

                var dieticianPatient = new DieticianPatient
                {
                    PatientId = request.InvitationPutDTO.PatientId,
                    DieticianId = request.InvitationPutDTO.DieticianId
                };

                _context.DieticianPatientsDb.Add(dieticianPatient);


                try
                {
                    var saveResult = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!saveResult)
                    {
                        return Result<InvitationPutDTO>.Failure("Nie udało się utworzyć powiązania Dietician-Patient.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia powiązania dietician-patient: " + ex);
                    return Result<InvitationPutDTO>.Failure("Wystąpił błąd dodawania powiązania. " + ex);
                }

                return Result<InvitationPutDTO>.Success(_mapper.Map<InvitationPutDTO>(invitation));
            }
        }
    }
}