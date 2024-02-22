using Application.Core;
using Application.DTOs.RoleDTO;
using Application.Validators.Role;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Roles
{
    public class RoleCreate
    {
        public class Command : IRequest<Result<RolePostDTO>>
        {
            public RolePostDTO RolePostDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<RolePostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly RoleCreateValidator _validator;

            public Handler(DietContext context, IMapper mapper, RoleCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<RolePostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.RolePostDTO);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<RolePostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var role = _mapper.Map<Role>(request.RolePostDTO);

                if (role == null)
                {
                    return Result<RolePostDTO>.Failure("Coś poszło nie tak z mapowaniem.");
                }

                role.Name = request.RolePostDTO.Name;
                role.whoAdded = "superAdmin";
                role.NormalizedName = request.RolePostDTO.Name.ToUpper();

                _context.Roles.Add(role);

                try
                {
                    var result = await _context.SaveChangesAsync() > 0;

                    if (!result)
                    {
                        return Result<RolePostDTO>.Failure("Operacja nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<RolePostDTO>.Failure("Wystąpił błąd podczas tworzenia roli.");
                }

                return Result<RolePostDTO>.Success(_mapper.Map<RolePostDTO>(role));
            }
        }
    }
}