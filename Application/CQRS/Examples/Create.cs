using Application.Core;
using DietDB;
using FluentValidation;
using MediatR;
using ModelsDB;

namespace Application.CQRS.Examples
{
    public class Create
    {
        public class Command : IRequest<PatientUpdateDTO<Unit>>
        {
            public Example Example { get; set; }
        }
        public class CommandValidator : AbstractValidator<Example>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Imie wymagane");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Opis wymagany");
            }
        }
        public class Hendler : IRequestHandler<Command, PatientUpdateDTO<Unit>>
        {
            private readonly DietContext _context;

            public Hendler(DietContext context)
            {
                _context = context;
            }

            public async Task<PatientUpdateDTO<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Examples.Add(request.Example);

                var result=await _context.SaveChangesAsync()>0;

                if (!result) return PatientUpdateDTO<Unit>.Failure("Nie udalo sie zapisac example");

                return PatientUpdateDTO<Unit>.Success(Unit.Value);
            }
        }
    }
}
