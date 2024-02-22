using Application.Core;
using Application.DTOs.IngredientDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Ingredients
{
    public class IngredientDelete
    {
        public class Command : IRequest<Result<IngredientDeleteDTO>>
        {
            public int IngredientId { get; set; }
            public IngredientDeleteDTO IngredientDeleteDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<IngredientDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<IngredientDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var ingredient = await _context.IngredientsDb
                        .SingleOrDefaultAsync(di => di.Id == request.IngredientId);

                    if (ingredient == null)
                    {
                        return Result<IngredientDeleteDTO>.Failure("Nie znaleziono składnika.");
                    }

                    var relations = _context.DishIngredientsDb.Any(di => di.IngredientId == ingredient.Id);

                    if (relations)
                    {
                        return Result<IngredientDeleteDTO>.Failure("Składnik jest uzywany w innej tabeli. Nie mozna usunąć.");
                    }

                    ingredient.isActive = false;

                    var nutrients = _context.IngredientNutrientsDb.Where(nu => nu.IngredientId == ingredient.Id);
                    foreach (var nutrient in nutrients)
                    {
                        nutrient.isActive = false;
                    }

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<IngredientDeleteDTO>.Failure("Operacja nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<IngredientDeleteDTO>.Failure("Wystąpił błąd podczas usuwania test results.");
                    }

                    return Result<IngredientDeleteDTO>.Success(_mapper.Map<IngredientDeleteDTO>(ingredient));
                }
            }
        }
    }
}