using Application.DTOs.DishDetailsToEditDTO;
using FluentValidation;

namespace Application.Validators.DishEditDetails
{
    public class DishIngredientDetailsUpdateValidator : AbstractValidator<DishIngredientsDetailsGetEditDTO>
    {
        public DishIngredientDetailsUpdateValidator()
        {
            RuleFor(dto => dto.IngredientName)
                .NotEmpty().WithMessage("Pole IngredientName nie może być puste.")
                .NotNull().WithMessage("Pole IngredientName nie może przyjmować null.")
                .MinimumLength(3).WithMessage("Minimalna długość Name to 3.")
                .When(dto => dto.IngredientName != null);
        }
    }
}