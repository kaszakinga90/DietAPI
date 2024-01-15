using Application.Core;
using Application.DTOs.DieticianDTO;
using Application.Services;
using Application.Validators.Dietician;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Application.CQRS.Dieticians
{
    public class DieticianEdit
    {
        public class Command : IRequest<Result<DieticianEditDTO>>
        {
            public DieticianEditDTO DieticianEditDTO { get; set; }
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<DieticianEditDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;
            private readonly DieticianUpdateValidator _validator;

            public Handler(DietContext context, IMapper mapper, ImageService imageService, DieticianUpdateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
                _validator = validator;
            }

            public async Task<Result<DieticianEditDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                //var validationResult = await _validator
                //    .ValidateAsync(request.DieticianEditDTO, cancellationToken);

                //if (!validationResult.IsValid)
                //{
                //    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                //    return Result<DieticianEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                //}

                var dietician = await _context.DieticiansDb
                    .FindAsync(new object[] { request.DieticianEditDTO.Id }, cancellationToken);

                if (dietician == null)
                {
                    return Result<DieticianEditDTO>.Failure("Dietetyk o podanym ID nie został znaleziony.");
                }

                _mapper.Map(request.DieticianEditDTO, dietician);

                // Obsługa obrazu
                if (request.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.File);
                    if (imageResult.Error != null)
                    {
                        return Result<DieticianEditDTO>.Failure(imageResult.Error.Message);
                    }

                    if (!string.IsNullOrEmpty(dietician.PublicId))
                    {
                        await _imageService.DeleteImageAsync(dietician.PublicId);
                    }

                    dietician.PictureUrl = imageResult.SecureUrl.ToString();
                    dietician.PublicId = imageResult.PublicId;
                }

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<DieticianEditDTO>.Failure("Edycja dietetyka nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DieticianEditDTO>.Failure("Wystąpił błąd podczas edycji dietetyka. " + ex);
                }
                return Result<DieticianEditDTO>.Success(_mapper.Map<DieticianEditDTO>(dietician));
            }
        }
    }
}
