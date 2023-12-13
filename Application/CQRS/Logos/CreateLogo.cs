using Application.Core;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.LogoDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using ModelsDB;
using ModelsDB.Functionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var logo = _mapper.Map<Logo>(request.LogoPostDTO);

                if (request.LogoPostDTO.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.LogoPostDTO.File);
                    //if (imageResult.Error != null)
                    //{
                    //    return OkResult();
                    //}

                    logo.PictureUrl = imageResult.SecureUrl.ToString();
                    logo.PublicId = imageResult.PublicId;
                }
                _context.LogosDb.Add(logo);
                await _context.SaveChangesAsync();
                return Result<LogoPostDTO>.Success(_mapper.Map<LogoPostDTO>(logo));
            }
        }
    }
}
