using Application.Core;
using Application.DTOs.DishDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Dishes
{
    public class DishesList
    {
        public class Query : IRequest<Result<List<DishGetDTO>>>
        {
            public int DieteticianId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<DishGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DishGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dishesList = await _context.DishesDb
                    .Where(d => d.DieticianId == null || d.DieticianId == request.DieteticianId)
                    .Select(d => new DishGetDTO
                    {
                        Id = d.Id,
                        Name = d.Name,
                    })
                    .ToListAsync(cancellationToken);

                if (dishesList == null)
                {
                    return Result<List<DishGetDTO>>.Failure("no results");

                }

                return Result<List<DishGetDTO>>.Success(dishesList);
            }
        }
    }
}

