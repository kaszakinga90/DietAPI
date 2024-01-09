using Application.Core;
using Application.DTOs.RoleDTO;
using Application.DTOs.UsersDTO.UserRoleDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users
{
    public class UserDetails
    {
        public class Query : IRequest<Result<UserRoleGetDTO>>
        {
            public int UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<UserRoleGetDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<UserRoleGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Where(u => u.Id == request.UserId)
                    .Select(u => new UserRoleGetDTO
                    {
                        Id = u.Id,
                        Email = u.Email,
                        UserRolesDTO = _context.UserRoles
                            .Where(ur => ur.UserId == u.Id)
                            .Select(ur => new RoleGetDTO
                            {
                                Id = ur.RoleId,
                                Name = _context.Roles
                                .Where(r => r.Id == ur.RoleId)
                                .Select(r => r.Name)
                                .FirstOrDefault()
                            })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Result<UserRoleGetDTO>.Failure("User z rolami nie został znaleziony.");
                }

                return Result<UserRoleGetDTO>.Success(user);
            }
        }
    }
}
