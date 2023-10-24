using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.SingleDiets
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
<<<<<<< HEAD:Application/CQRS/SingleDiets/SingleDietCreate.cs
                _context.SingleDiet.Add(request.SingleDiet);

=======
                _context.Examples.Add(request.Example);
>>>>>>> 6d047e7594153c95fdeecf218c13172cd7de5b09:Application/Examples/Create.cs
                await _context.SaveChangesAsync();
            }
        }
    }
}
