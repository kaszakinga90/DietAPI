using Application.DTOs.DishDetailsToEditDTO;
using FluentValidation;

namespace Application.Validators.DishEditDetails
{
    public class DishRecipeDetailsUpdateValidator : AbstractValidator<RecipeStepGetEditDTO>
    {
        public DishRecipeDetailsUpdateValidator()
        {
            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Pole Description nie może być puste.")
                .NotNull().WithMessage("Pole Description nie może przyjmować null.")
                .MinimumLength(3).WithMessage("Minimalna długość Description to 3.")
                .When(dto => dto.Description != null);
        }
    }
}