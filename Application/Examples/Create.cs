using DietDB;
using MediatR;
using ModelsDB;

namespace Application.Examples
{
    public class Create
    {
        public class Command : IRequest
        {
            public Example Example { get; set; }
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
                _context.Examples.Add(request.Example);
                await _context.SaveChangesAsync();
            }
        }
    }
}
