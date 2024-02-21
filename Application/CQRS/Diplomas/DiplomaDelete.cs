using Application.Core;
using Application.DTOs.DiplomaDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Diplomas
{
    public class DiplomaDelete
    {
        public class Command : IRequest<Result<DiplomaDeleteDTO>>
        {
            public int DieticianId { get; set; }
            public int DiplomaId { get; set; }
            public DiplomaDeleteDTO DiplomaDeleteDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<DiplomaDeleteDTO>>
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

                public async Task<Result<DiplomaDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var diploma = await _context.DiplomasDb
                        .SingleOrDefaultAsync(di => di.Id == request.DiplomaId && di.DieticianId == request.DieticianId);

                    if (diploma == null)
                    {
                        return Result<DiplomaDeleteDTO>.Failure("Nie znaleziono dyplomu.");
                    }

                    _mapper.Map(request.DiplomaDeleteDTO, diploma);

                    if (diploma != null)
                    {
                        var imageResult = await _imageService.DeleteImageAsync(diploma.PublicId);

                        if (imageResult.Error != null)
                        {
                            return Result<DiplomaDeleteDTO>.Failure(imageResult.Error.Message);
                        }

                        _context.DiplomasDb.Remove(diploma);

                        try
                        {
                            var result = await _context.SaveChangesAsync() > 0;
                            if (!result)
                            {
                                return Result<DiplomaDeleteDTO>.Failure("Operacja nie powiodła się.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                            return Result<DiplomaDeleteDTO>.Failure("Wystąpił błąd podczas usuwania test results.");
                        }
                    }
                    return Result<DiplomaDeleteDTO>.Success(_mapper.Map<DiplomaDeleteDTO>(diploma));
                }
            }
        }
    }
}