﻿using Application.Core;
using Application.DTOs.DiplomaDTO;
using Application.Services;
using Application.Validators.Diploma;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using ModelsDB;
using System.Diagnostics;

namespace Application.CQRS.Diplomas
{
    public class DiplomaCreate
    {
        public class Command : IRequest<Result<DiplomaPostDTO>>
        {
            public DiplomaPostDTO DiplomaPostDTO { get; set; }
            public IFormFile File { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<DiplomaPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;
            private readonly DiplomaCreateValidator _validator;

            public Handler(DietContext context, IMapper mapper, ImageService imageService, DiplomaCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
                _validator = validator;
            }

            public async Task<Result<DiplomaPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.DiplomaPostDTO);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<DiplomaPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                try
                {
                    var diploma = _mapper.Map<Diploma>(request.DiplomaPostDTO);

                    if (request.DiplomaPostDTO.File != null)
                    {
                        var imageResult = await _imageService.AddImageAsync(request.DiplomaPostDTO.File);

                        if (imageResult.Error != null)
                        {
                            return Result<DiplomaPostDTO>.Failure(imageResult.Error.Message);
                        }

                        diploma.PictureUrl = imageResult.SecureUrl.ToString();
                        diploma.PublicId = imageResult.PublicId;
                    }
                    _context.DiplomasDb.Add(diploma);
                    await _context.SaveChangesAsync();
                    return Result<DiplomaPostDTO>.Success(_mapper.Map<DiplomaPostDTO>(diploma));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DiplomaPostDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}