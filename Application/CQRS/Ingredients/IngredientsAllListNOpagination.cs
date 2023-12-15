using Application.Core;
using Application.DTOs.FoodCatalogDTO;
using Application.DTOs.IngredientDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var ingredients = await _context.IngredientsDb
                    .Where(m => m.DieticianId == null || m.DieticianId == request.DieteticianId)
                    .Select(m => new IngredientGetDTO
                    {
                        Id = m.Id,
                        IngredientName=m.Name,
                    })
                    .ToListAsync(cancellationToken);
                return Result<List<IngredientGetDTO>>.Success(ingredients);
            }
        }
    }
}
