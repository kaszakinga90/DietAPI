using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.CategoryOfDiets
{
    public class CategoryOfDietDetails
    {
        public class Query : IRequest<CategoryOfDiet>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CategoryOfDiet>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<CategoryOfDiet> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.CategoryOfDiet.FindAsync(request.Id);
            }
        }
    }
}
