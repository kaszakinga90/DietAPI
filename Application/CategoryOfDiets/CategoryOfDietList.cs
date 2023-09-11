using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Functionality;

namespace Application.CategoryOfDiets
{
    public class CategoryOfDietList
    {
        public class Query : IRequest<List<CategoryOfDiet>> { }

        public class Handler : IRequestHandler<Query, List<CategoryOfDiet>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<CategoryOfDiet>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.CategoryOfDiet.ToListAsync(cancellationToken);
            }
        }
    }
}
