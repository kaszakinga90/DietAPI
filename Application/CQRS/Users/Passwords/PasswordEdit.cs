using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ModelsDB;
using System.Diagnostics;

namespace Application.CQRS.Users.Passwords
{
    public class PasswordEdit
    {
        public class Command : IRequest<Result<PasswordEditDTO>>
        {
            public int UserId { get; set; }
            public PasswordEditDTO PasswordEditDTO { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<PasswordEditDTO>>
        {
            private readonly UserManager<User> _userManager;


            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Result<PasswordEditDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (user == null)
                {
                    return Result<PasswordEditDTO>.Failure("User o podanym ID nie został znaleziony.");
                }

                try
                {
                    var result = await _userManager.ChangePasswordAsync(user, request.PasswordEditDTO.OldPassword, request.PasswordEditDTO.NewPassword);

                    if (!result.Succeeded)
                    {
                        var errors = result.Errors.Select(e => e.Description);
                        var errorMessage = string.Join(", ", errors);

                        return Result<PasswordEditDTO>.Failure($"Zmiana hasła nie powiodła się. Błędy: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Błąd podczas zmiany hasła: " + ex);
                    return Result<PasswordEditDTO>.Failure("Wystąpił błąd podczas zmiany hasła.");
                }

                var successDTO = new PasswordEditDTO
                {
                    OldPassword = request.PasswordEditDTO.OldPassword,
                    NewPassword = request.PasswordEditDTO.NewPassword
                };

                return Result<PasswordEditDTO>.Success(successDTO);
            }
        }
    }
}