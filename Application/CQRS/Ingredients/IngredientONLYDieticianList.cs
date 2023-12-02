using Application.Core;
using Application.DTOs.IngredientDTO;
using Application.FiltersExtensions.Ingredients;
using DietDB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Ingredients
{
    public class IngredientONLYDieticianList
    {
        public class Query : IRequest<Result<PagedList<IngredientGetDTO>>>
        {
            public int DieticianId { get; set; }
            public IngredientsParams Params { get; set; }

            public class Handler : IRequestHandler<Query, Result<PagedList<IngredientGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<PagedList<IngredientGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var ingridientList = _context.IngredientsDb
                         .Where(i => i.DieticianId == request.DieticianId)
                         .Select(i => new IngredientGetDTO
                         {
                             Id = i.Id,
                             IngredientName = i.Name,
                             Calories = i.Calories,
                             GlycemicIndex = i.GlycemicIndex ?? 0,
                             ServingQuantity = i.ServingQuantity ?? 0,
                             MeasureId = i.MeasureId,
                             PictureUrl = i.PictureUrl,
                         })
                        .AsQueryable();
                    ingridientList = ingridientList.Search(request.Params.SearchTerm);
                    return Result<PagedList<IngredientGetDTO>>.Success(await PagedList<IngredientGetDTO>.CreateAsync(ingridientList, request.Params.PageNumber, request.Params.PageSize));
                }
            }
        }
    }
}


