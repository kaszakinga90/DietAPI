using DietDB;
using MediatR;
using ModelsDB;
using Application.DTOs.UsersDTO.UserRoleDTO;
using Application.Core;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Application.Validators.UserRole;

namespace Application.CQRS.UserRoles
{
    public class UserRoleCreate
    {
        public class Command : IRequest<Result<UserRoleCreateDTO>>
        {
            public UserRoleCreateDTO UserRoleCreateDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<UserRoleCreateDTO>>
            {
                private readonly DietContext _context;
                private readonly UserManager<User> _userManager;
                private readonly UserRoleCreateValidate _userRoleValidate;

                public Handler(DietContext context, UserManager<User> userManager, UserRoleCreateValidate userRoleValidate)
                {
                    _context = context;
                    _userManager = userManager;
                    _userRoleValidate = userRoleValidate;
                }

                public async Task<Result<UserRoleCreateDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var validationResult = await _userRoleValidate
                        .ValidateAsync(request.UserRoleCreateDTO);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                        return Result<UserRoleCreateDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                    }

                    var user = await _userManager
                        .FindByIdAsync(request.UserRoleCreateDTO.UserId.ToString());

                    if (user == null)
                    {
                        return Result<UserRoleCreateDTO>.Failure("User o podanym id nie został znaleziony.");
                    }

                    var roleToAdd = await _context.Roles
                        .FindAsync(request.UserRoleCreateDTO.RoleId);

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
                            .Where(u => u.Id == request.UserRoleCreateDTO.UserId)
                            .Select(u => new UserRoleCreateDTO
                            {
                                UserId = u.Id,
                                RoleId = request.UserRoleCreateDTO.RoleId,
                                Name = _context.Roles
                                     .Where(r => r.Id == request.UserRoleCreateDTO.RoleId)
                                     .Select(r => r.Name)
                                     .FirstOrDefault()
                            })
                            .FirstOrDefaultAsync();

                    return Result<UserRoleCreateDTO>.Success(userAfterAddedRole);
                }
            }
        }
    }
}