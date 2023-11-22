using Application.Core;
using Application.DTOs.DishDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Dishes
{
    public class DishesList
    {
        public class Query : IRequest<Result<List<DishDTO>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<DishDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DishDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dish = await _context.DishesDb
                    .Where(d=>d.Id!=4)
                    .Select(d => new DishDTO
                    {
                        Id = d.Id,
                        Name = d.Name,
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<DishDTO>>.Success(dish);
            }
        }
    }
}

