using Application.Core;
using Application.DTOs.AdminDTO;
using Application.Validators.Admin;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ModelsDB;
using System.Diagnostics;

namespace Application.CQRS.Admins
{
    public class AdminCreate
    {
        public class Command : IRequest<Result<AdminPostDTO>>
        {
            public AdminPostDTO AdminPostDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<AdminPostDTO>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;
            private readonly AdminCreateValidator _validator;

            public Handler(UserManager<User> userManager, IMapper mapper, AdminCreateValidator validator)
            {
                _userManager = userManager;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<AdminPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.AdminPostDTO);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<AdminPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var admin = _mapper.Map<Admin>(request.AdminPostDTO);

                if (admin == null)
                {
                    return Result<AdminPostDTO>.Failure("Admin nie został znaleziony.");
                }

                admin.UserName = request.AdminPostDTO.Email;
                admin.EmailConfirmed = true;

                try
                {
                    var createUserResult = await _userManager.CreateAsync(admin, request.AdminPostDTO.Password);
                    if (!createUserResult.Succeeded)
                    {
                        return Result<AdminPostDTO>.Failure("Operacja nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<AdminPostDTO>.Failure("Wystąpił błąd podczas tworzenia admina.");
                }

                return Result<AdminPostDTO>.Success(_mapper.Map<AdminPostDTO>(admin));
            }
        }
    }
}