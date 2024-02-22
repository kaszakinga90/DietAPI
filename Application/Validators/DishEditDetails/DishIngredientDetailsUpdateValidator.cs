using Application.DTOs.DishDetailsToEditDTO;
using FluentValidation;

namespace Application.Validators.DishEditDetails
{
    public class DishIngredientDetailsUpdateValidator : AbstractValidator<DishIngredientsDetailsGetEditDTO>
    {
        public DishIngredientDetailsUpdateValidator()
        {
            RuleFor(dto => dto.DishId)
                .NotEmpty().WithMessage("Pole DishId nie może być puste.")
                .NotNull().WithMessage("Pole DishId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole DishId musi być liczbą całkowitą większą niż 0.");

            RuleFor(dto => dto.IngredientId)
                .NotEmpty().WithMessage("Pole IngredientId nie może być puste.")
                .NotNull().WithMessage("Pole IngredientId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole IngredientId musi być liczbą całkowitą większą niż 0.");
        }
    }
}