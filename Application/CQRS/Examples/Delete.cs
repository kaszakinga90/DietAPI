using Application.Core;
using DietDB;
using MediatR;

namespace Application.CQRS.Examples
{
    public class Delete
    {
        public class Command : IRequest<PatientUpdateDTO<Unit>>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, PatientUpdateDTO<Unit>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }
            public async Task<PatientUpdateDTO<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var example = await _context.Examples.FindAsync(request.Id);
                if (example == null) return null;

                _context.Remove(example);

                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return PatientUpdateDTO<Unit>.Failure("Nie udalo sie usunac example");

                return PatientUpdateDTO<Unit>.Success(Unit.Value);
            }
        }
    }
}
