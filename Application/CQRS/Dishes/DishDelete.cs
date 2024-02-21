using Application.Core;
using Application.DTOs.DishDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dishes
{
    public class DishDelete
    {
        public class Command : IRequest<Result<DishDeleteDTO>>
        {
            public int DishId { get; set; }
            public DishDeleteDTO DishDeleteDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<DishDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<DishDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var dish = await _context.DishesDb
                        .SingleOrDefaultAsync(di => di.Id == request.DishId, cancellationToken);

                    if (dish == null)
                    {
                        return Result<DishDeleteDTO>.Failure("Nie znaleziono dania.");
                    }

                    var relations = _context.MealTimesDb.Any(mt => mt.DishId == dish.Id);

                    if (relations)
                    {
                        return Result<DishDeleteDTO>.Failure("To danie jest uzywane w innej tabeli. Nie mozna usunąć.");
                    }

                    dish.isActive = false;

                    var relatedEntities = _context.DishFoodCatalogsDb.Where(df => df.DishId == dish.Id);
                    foreach (var dishFoodCatalog in relatedEntities)
                    {
                        dishFoodCatalog.isActive = false;
                    }

                    var dishIngredients = _context.DishIngredientsDb.Where(di => di.DishId == dish.Id);
                    foreach (var dishIngredient in dishIngredients)
                    {
                        dishIngredient.isActive = false;
                    }

                    var existingRecipe = await _context.RecipesDb.FindAsync(dish.RecipeId);
                    if (existingRecipe != null)
                    {
                        existingRecipe.isActive = false;
                    }

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<DishDeleteDTO>.Failure("Operacja nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<DishDeleteDTO>.Failure("Wystąpił błąd podczas usuwania test results.");
                    }
                    
                    return Result<DishDeleteDTO>.Success(_mapper.Map<DishDeleteDTO>(dish));
                }
            }
        }
    }
}