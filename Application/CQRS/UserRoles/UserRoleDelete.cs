using Application.Core;
using Application.DTOs.RoleDTO;
using Application.DTOs.UsersDTO.UserRoleDTO;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using System.Diagnostics;

namespace Application.CQRS.UserRoles
{
    public class UserRoleDelete
    {
        public class Command : IRequest<Result<RoleForUserDeleteDTO>>
        {
            public int UserRoleId { get; set; }
            public int UserId { get; set; }

            public class Handler : IRequestHandler<Command, Result<RoleForUserDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly UserManager<User> _userManager;

                public Handler(DietContext context, UserManager<User> userManager)
                {
                    _context = context;
                    _userManager = userManager;
                }

                public async Task<Result<RoleForUserDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var roleToRemove = await _context.Roles.FindAsync(request.UserRoleId);

                    var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                    if (user == null)
                    {
                        return Result<RoleForUserDeleteDTO>.Failure("User o podanym id nie został znaleziony.");
                    }
                    if (roleToRemove == null)
                    {
                        return Result<RoleForUserDeleteDTO>.Failure("Rola o podanym id nie została znaleziona.");
                    }

                    try
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, roleToRemove.ToString());

                        if (!result.Succeeded)
                        {
                            return Result<RoleForUserDeleteDTO>.Failure("Usunięcie uprawnienia nie powiodło się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowidzenia: " + ex);
                        return Result<RoleForUserDeleteDTO>.Failure("Wystąpił błąd podczas edycji uprawnień.");
                    }

                    var userAfterDeleteRole = await _context.Users
                            .Where(u => u.Id == user.Id)
                            .Select(u => new RoleForUserDeleteDTO
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

                    return Result<RoleForUserDeleteDTO>.Success(userAfterDeleteRole);
                }
            }
        }
    }
}