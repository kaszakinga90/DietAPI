using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.SingleDiets
{
    public class SingleDietCreate
    {
        public class Command : IRequest
        {
            public SingleDiet SingleDiet { get; set; }
        }
        public class Hendler : IRequestHandler<Command>
        {
            private readonly DietContext _context;

            public Hendler(DietContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.SingleDiet.Add(request.SingleDiet);

                await _context.SaveChangesAsync();
            }
        }
    }
}
