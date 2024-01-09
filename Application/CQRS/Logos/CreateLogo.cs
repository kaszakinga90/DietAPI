using Application.Core;
using Application.DTOs.LogoDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Functionality;

// TODO : obsługa - dokończ. funkcjonalności
namespace Application.CQRS.Logos
{
    public class CreateLogo
    {
        public class Command : IRequest<Result<LogoPostDTO>>
        {
            public LogoPostDTO LogoPostDTO { get; set; }
            public IFormFile File { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<LogoPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;
            public Handler(DietContext context, IMapper mapper, ImageService imageService)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
            }
            public async Task<Result<LogoPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                Logo logo = await _context.LogosDb
                    .FirstOrDefaultAsync(l => l.DieticianId == request.LogoPostDTO.DieticianId);

                if (logo == null)
                {
                    logo = new Logo();
                    _context.LogosDb.Add(logo);
                }

                _mapper.Map(request.LogoPostDTO, logo);

                if (request.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.File);
                    if (imageResult.Error != null)
                    {
                        return Result<LogoPostDTO>.Failure(imageResult.Error.Message);
                    }

                    if (!string.IsNullOrEmpty(logo.PublicId))
                    {
                        await _imageService.DeleteImageAsync(logo.PublicId);
                    }

                    logo.PictureUrl = imageResult.SecureUrl.ToString();
                    logo.PublicId = imageResult.PublicId;
                }

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<LogoPostDTO>.Failure("Edycja logo nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    return Result<LogoPostDTO>.Failure("Wystąpił błąd podczas edycji logo: " + ex.Message);
                }

                return Result<LogoPostDTO>.Success(_mapper.Map<LogoPostDTO>(logo));
            }
        }
    }
}
