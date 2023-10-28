using FluentValidation;
using ModelsDB;

namespace Application.CQRS.Examples
{
    public class ExampleValidator : AbstractValidator<Example>
    {
        public ExampleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Imie wymagane");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Opis wymagany");
        }
    }
}
