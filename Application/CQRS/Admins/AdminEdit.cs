using Application.Core;
using Application.DTOs.AdminDTO;
using Application.Services;
using Application.Validators.Admin;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Application.CQRS.Admins
{
    public class AdminEdit
    {
        public class Command : IRequest<Result<AdminEditDTO>>
        {
            public AdminEditDTO Admin { get; set; }
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<AdminEditDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;
            private readonly AdminUpdateValidator _validator;

            public Handler(DietContext context, IMapper mapper, ImageService imageService, AdminUpdateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
                _validator = validator;
            }

            public async Task<Result<AdminEditDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.Admin, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<AdminEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var admin = await _context.AdminsDb
                    .FindAsync(new object[] { request.Admin.Id }, cancellationToken);

                if (admin == null)
                {
                    return Result<AdminEditDTO>.Failure("Admin o podanym ID nie został znaleziony.");
                }

                _mapper.Map(request.Admin, admin);

                if (request.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.File);

                    if (imageResult.Error != null)
                    {
                        return Result<AdminEditDTO>.Failure(imageResult.Error.Message);
                    }

                    if (!string.IsNullOrEmpty(admin.PublicId))
                    {
                        await _imageService.DeleteImageAsync(admin.PublicId);
                    }

                    admin.PictureUrl = imageResult.SecureUrl.ToString();
                    admin.PublicId = imageResult.PublicId;
                }

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<AdminEditDTO>.Failure("Edycja admina nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<AdminEditDTO>.Failure("Wystąpił błąd podczas edycji admina.");
                }

                return Result<AdminEditDTO>.Success(_mapper.Map<AdminEditDTO>(admin));
            }
        }
    }
}