﻿using Application.Core;
using Application.DTOs.FoodCatalogDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.FoodCatalogs
{
    public class FoodCatalogList
    {
        public class Query : IRequest<Result<List<FoodCatalogGetDTO>>> {
            public int DieteticianId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<FoodCatalogGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<FoodCatalogGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var foodCatalog = await _context.FoodCatalogsDb
                    .Where(m => m.DieticianId == null || m.DieticianId == request.DieteticianId)
                    .Select(m => new FoodCatalogGetDTO
                    {
                        Id = m.Id,
                        CatalogName = m.CatalogName
                    })
                    .ToListAsync(cancellationToken);
                return Result<List<FoodCatalogGetDTO>>.Success(foodCatalog);
            }
        }
    }
}
