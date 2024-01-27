using Application.Core;
using Application.DTOs.IngredientDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Ingredients
{
    public class IngredientsAllListNOpagination
    {
        public class Query : IRequest<Result<List<IngredientGetDTO>>>
        {
            public int DieteticianId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<IngredientGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<IngredientGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var ingredients = await _context.IngredientsDb
                    .Where(m => m.DieticianId == null || m.DieticianId == request.DieteticianId)
                    .Select(m => new IngredientGetDTO
                    {
                        Id = m.Id,
                        Name = m.Name,
                    })
                    .ToListAsync(cancellationToken);

                    return Result<List<IngredientGetDTO>>.Success(ingredients);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<IngredientGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}