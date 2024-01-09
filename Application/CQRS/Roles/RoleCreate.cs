using Application.Core;
using Application.DTOs.AdminDTO;
using Application.DTOs.RoleDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ModelsDB;
using ModelsDB.Functionality;
using System.Diagnostics;
using System.Threading;

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

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<RolePostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var role = _mapper.Map<Role>(request.RolePostDTO);

                role.Name = request.RolePostDTO.Name;
                role.whoAdded = "superAdmin";
                role.NormalizedName = request.RolePostDTO.Name.ToUpper();

                _context.Roles.Add(role);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;

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