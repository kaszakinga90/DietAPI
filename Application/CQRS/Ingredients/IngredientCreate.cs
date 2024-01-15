using Application.Core;
using Application.DTOs.IngredientDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using ModelsDB;
using System.Diagnostics;

namespace Application.CQRS.Ingredients
{
    public class IngredientCreate
    {
        public class Command : IRequest<Result<IngredientDTO>>
        {
            public IngredientDTO IngredientDTO { get; set; }
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<IngredientDTO>>
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

            public async Task<Result<IngredientDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var newIngredient = _mapper.Map<Ingredient>(request.IngredientDTO);

                if (newIngredient == null)
                {
                    return Result<IngredientDTO>.Failure("Niepowodzenie mapowania.");
                }

                _context.IngredientsDb.Add(newIngredient);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<IngredientDTO>.Failure("Operacja nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<IngredientDTO>.Failure("Wystąpił błąd podczas dodawania ingredient.");
                }

                return Result<IngredientDTO>.Success(_mapper.Map<IngredientDTO>(newIngredient));
            }
        }
    }
}