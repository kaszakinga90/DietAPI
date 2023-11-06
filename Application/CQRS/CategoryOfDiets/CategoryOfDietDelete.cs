using DietDB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.CategoryOfDiets
{
    public class CategoryOfDietDelete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var categoryOfDiet = await _context.CategoryOfDietsDb.FindAsync(request.Id);

                _context.Remove(categoryOfDiet);

                await _context.SaveChangesAsync();
            }
        }
    }
}
