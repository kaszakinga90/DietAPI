using Application.Core;
using Application.DTOs.IngredientDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Ingredients
{
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
                    try
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
                            Weight = m.Weight ?? 0,
                            UnitId = m.UnitId,
                        })
                        .FirstOrDefaultAsync();

                        if (ingredient == null)
                        {
                            return Result<IngredientGetDTO>.Failure("Nie znaleziono składnika.");
                        }

                        return Result<IngredientGetDTO>.Success(ingredient);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<IngredientGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}