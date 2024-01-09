using DietDB;
using MediatR;
using ModelsDB;
using Application.DTOs.UsersDTO.UserRoleDTO;
using Application.Core;
using Application.DTOs.RoleDTO;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.UserRoles
{
    public class UserRoleCreate
    {
        public class Command : IRequest<Result<UserRoleCreateDTO>>
        {
            public int UserId { get; set; }
            public int RoleId { get; set; }

            public class Handler : IRequestHandler<Command, Result<UserRoleCreateDTO>>
            {
                private readonly DietContext _context;
                private readonly UserManager<User> _userManager;

                public Handler(DietContext context, UserManager<User> userManager)
                {
                    _context = context;
                    _userManager = userManager;
                }

                public async Task<Result<UserRoleCreateDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                    if (user == null)
                    {
                        return Result<UserRoleCreateDTO>.Failure("User o podanym id nie został znaleziony.");
                    }

                    var roleToAdd = await _context.Roles.FindAsync(request.RoleId);

                    if (roleToAdd == null)
                    {
                        return Result<UserRoleCreateDTO>.Failure("Rola o podanym id nie została znaleziona.");
                    }

                    try
                    {
                        var result = await _userManager.AddToRoleAsync(user, roleToAdd.ToString());

                        if (!result.Succeeded)
                        {
                            return Result<UserRoleCreateDTO>.Failure("Dodanie uprawnienia nie powiodło się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowidzenia: " + ex);
                        return Result<UserRoleCreateDTO>.Failure("Wystąpił błąd podczas edycji uprawnień.");
                    }

                    var userAfterAddedRole = await _context.Users
                            .Where(u => u.Id == request.UserId)
                            .Select(u => new UserRoleCreateDTO
                            {
                                UserId = u.Id,
                                Email = u.Email,
                                RoleId = request.RoleId,
                                RoleName = _context.Roles
                                     .Where(r => r.Id == request.RoleId)
                                     .Select(r => r.Name)
                                     .FirstOrDefault()
                            })
                            .FirstOrDefaultAsync(cancellationToken);

                    return Result<UserRoleCreateDTO>.Success(userAfterAddedRole);
                }
            }
        }
    }
}
