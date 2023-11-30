using Application.Core;
using Application.DTOs.DiplomaDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using ModelsDB;

namespace Application.CQRS.Diplomas
{
    public class CreateDiploma
    {
        public class Command : IRequest<Result<DiplomaPostDTO>>
        {
            public DiplomaPostDTO DiplomaDTO { get; set; }
            public IFormFile File { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<DiplomaPostDTO>>
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
            public async Task<Result<DiplomaPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var diploma = _mapper.Map<Diploma>(request.DiplomaDTO);

                if (request.DiplomaDTO.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.DiplomaDTO.File);
                    //if (imageResult.Error != null)
                    //{
                    //    return OkResult();
                    //}

                    diploma.PictureUrl = imageResult.SecureUrl.ToString();
                    diploma.PublicId = imageResult.PublicId;
                }
                _context.DiplomasDb.Add(diploma);
                 await _context.SaveChangesAsync();
                return Result<DiplomaPostDTO>.Success(_mapper.Map<DiplomaPostDTO>(diploma));
            }
        }
    }
}
