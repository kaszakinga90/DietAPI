using Application.Core;
using Application.DTOs.AdminDTO;
using Application.DTOs.InvitationDTO;
using Application.Validators.Invitation;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.Invitations
{
    public class InvitationCreate
    {
        public class Command : IRequest<Result<InvitationPostDTO>>
        {
            public InvitationPostDTO InvitationPostDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<InvitationPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly InvitationCreateValidator _validator;

            public Handler(DietContext context, IMapper mapper, InvitationCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<InvitationPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.InvitationPostDTO, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<InvitationPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var invitation = _mapper.Map<Invitation>(request.InvitationPostDTO);

                invitation.IsAccepted = false;
                invitation.IsDeclined = false;
                invitation.IsSended = true;

                var dietetician = await _context.DieticiansDb
                    .FindAsync(request.InvitationPostDTO.DieticianId);

                if (dietetician == null)
                {
                    return Result<InvitationPostDTO>.Failure("Dietetyk nie został znaleziony.");
                }

                _context.InvitationsDb.Add(invitation);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<InvitationPostDTO>.Failure("Stworzenie zaproszenia nie powiodło się.");
                    }
                }
                catch (Exception ex)
                {
                    return Result<InvitationPostDTO>.Failure("Wystąpił błąd podczas tworzenia zaproszenia: " + ex.Message);
                }

                return Result<InvitationPostDTO>.Success(_mapper.Map<InvitationPostDTO>(invitation));
            }
        }
    }
}
//namespace Application.CQRS.Invitations
//{
//    public class InvitationCreate
//    {
//        public class Command : IRequest<Result<InvitationPostDTO>>
//        {
//            public InvitationPostDTO InvitationPostDTO { get; set; }
//        }

//        public class Handler : IRequestHandler<Command, Result<InvitationPostDTO>>
//        {
//            private readonly DietContext _context;
//            private readonly IMapper _mapper;

//            public Handler(DietContext context, IMapper mapper)
//            {
//                _context = context;
//                _mapper = mapper;
//            }

//            public async Task<Result<InvitationPostDTO>> Handle(Command request, CancellationToken cancellationToken)
//            {
//                var invitation = _mapper.Map<Invitation>(request.InvitationPostDTO);

//                invitation.IsAccepted = false;
//                invitation.IsDeclined = false;
//                invitation.IsSended = true;

//                var dietetician = await _context.DieticiansDb.FindAsync(request.InvitationPostDTO.DieticianId);
//                if (dietetician == null)
//                {
//                    throw new Exception("Dietetyk nie został znaleziony");
//                }

//                _context.InvitationsDb.Add(invitation);

//                try
//                {
//                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
//                    if (!result)
//                    {
//                        return Result<InvitationPostDTO>.Failure("Stworzenie zaproszenia nie powiodło się.");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    return Result<InvitationPostDTO>.Failure("Wystąpił błąd podczas tworzenia zaproszenia: " + ex.Message);
//                }

//                return Result<InvitationPostDTO>.Success(_mapper.Map<InvitationPostDTO>(invitation));
//            }
//        }
//    }
//}