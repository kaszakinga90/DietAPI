using Application.DTOs.DishDetailsToEditDTO;
using FluentValidation;

namespace Application.Validators.DishEditDetails
{
    public class DishFoodCatalogDetailsUpdateValidator : AbstractValidator<DishFoodCatalogsDetailsGetEditDTO>
    {
        public DishFoodCatalogDetailsUpdateValidator()
        {
            RuleFor(dto => dto.DishId)
                .NotEmpty().WithMessage("Pole DishId nie może być puste.")
                .NotNull().WithMessage("Pole DishId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole DishId musi być liczbą całkowitą większą niż 0.");

            RuleFor(dto => dto.FoodCatalogId)
                .NotEmpty().WithMessage("Pole FoodCatalogId nie może być puste.")
                .NotNull().WithMessage("Pole FoodCatalogId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole FoodCatalogId musi być liczbą całkowitą większą niż 0.");
        }
    }
}
