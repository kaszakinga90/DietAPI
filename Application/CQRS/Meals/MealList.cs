using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Functionality;

namespace Application.CQRS.Meals
{
    public class MealList
    {
        public class Query : IRequest<List<Meal>> { }

        public class Handler : IRequestHandler<Query, List<Meal>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<Meal>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.MealsDb.ToListAsync(cancellationToken);
            }
        }
    }
}
