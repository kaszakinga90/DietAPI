using Application.Core;
using AutoMapper;
using DietDB;
using FluentValidation;
using MediatR;
using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Examples
{
    public class Edit
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
        public class Handler : IRequestHandler<Command, PatientUpdateDTO<Unit>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PatientUpdateDTO<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var example = await _context.Examples.FindAsync(request.Example.Id);
                if (example == null) return null;
                _mapper.Map(request.Example, example);
               var result = await _context.SaveChangesAsync()>0;
                if (!result) return PatientUpdateDTO<Unit>.Failure("Edycja example nie powiodla sie");

                return PatientUpdateDTO<Unit>.Success(Unit.Value);
            }
        }
    }
}
