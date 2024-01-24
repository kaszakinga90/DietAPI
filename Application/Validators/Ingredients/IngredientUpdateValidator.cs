using Application.CQRS.Ingredients;
using FluentValidation;

namespace Application.Validators.Ingredients
{
    public class IngredientUpdateValidator : AbstractValidator<IngredientEditDTO>
    {
        public IngredientUpdateValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Pole Id nie może być puste.")
                .NotNull().WithMessage("Pole Id nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole Id musi mieć wartość większą niż 0");
        }
    }
}