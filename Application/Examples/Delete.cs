using DietDB;
using MediatR;

namespace Application.Examples
{
    public class Delete
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
                var example=await _context.Examples.FindAsync(request.Id);

                _context.Remove(example);

                await _context.SaveChangesAsync();
            }
        }
    }
}
