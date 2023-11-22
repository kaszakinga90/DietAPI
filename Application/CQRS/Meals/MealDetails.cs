using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.Meals
{
    public class MealDetails
    {
        public class Query : IRequest<Meal>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Meal>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Meal> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.MealsDb.FindAsync(request.Id);
            }
        }
    }
}
