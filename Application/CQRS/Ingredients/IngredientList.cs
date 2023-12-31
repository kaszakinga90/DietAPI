﻿using Application.Core;
using Application.DTOs.IngredientDTO;
using Application.FiltersExtensions.Ingredients;
using DietDB;
using MediatR;

namespace Application.CQRS.Ingredients
{
    public class IngredientList
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
                         .Where(i => i.DieticianId == null)
                         .Select(i => new IngredientGetDTO
                         {
                             Id = i.Id,
                             IngredientName = i.Name,
                             Calories = i.Calories,
                             GlycemicIndex = i.GlycemicIndex ?? 0,
                             ServingQuantity = i.ServingQuantity ?? 0,
                             MeasureId = i.MeasureId,
                             PictureUrl = i.PictureUrl,
                             Weight = i.Weight,
                         })
                        .AsQueryable();
                    ingridientList = ingridientList.Search(request.Params.SearchTerm);
                    return Result<PagedList<IngredientGetDTO>>.Success(await PagedList<IngredientGetDTO>.CreateAsync(ingridientList, request.Params.PageNumber, request.Params.PageSize));
                }
            }
        }
    }
}

