using Application.Core;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.IngredientDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ingredients
{
    /// <summary>
    /// Zawiera klasy służące do pobierania szczegółów produktu(składnika) na podstawie jego identyfikatora.
    /// </summary>
    public class IngredientDetails
    {
        public class Query : IRequest<Result<IngredientGetDTO>>
        {
            public int IngredientId { get; set; }

            public class Handler : IRequestHandler<Query, Result<IngredientGetDTO>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<IngredientGetDTO>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var ingredient = await _context.IngredientsDb
                        .Where(m => m.Id == request.IngredientId)
                        .Select(m => new IngredientGetDTO
                        {
                            Id = m.Id,
                            IngredientName = m.Name,
                            Calories = m.Calories,
                            GlycemicIndex = m.GlycemicIndex ?? 0,
                            ServingQuantity = m.ServingQuantity ?? 0,
                            MeasureId = m.MeasureId,
                            PictureUrl = m.PictureUrl,
                            Weight = m.Weight??0,
                        })
                        .FirstOrDefaultAsync();

                    if (ingredient == null)
                    {
                        return Result<IngredientGetDTO>.Failure("Ingredient not found.");
                    }

                    return Result<IngredientGetDTO>.Success(ingredient);
                }

            }
        }
    }
}

