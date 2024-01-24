using Application.DTOs.FoodCatalogDTO;
using FluentValidation;

namespace Application.Validators.FoodCatalog
{
    public class FoodCatalogDieticianUpdateValidator : AbstractValidator<FoodCatalogDieticianEditDTO>
    {
        public FoodCatalogDieticianUpdateValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 0.");

            RuleFor(dto => dto.CatalogName)
                .NotEmpty().WithMessage("Pole CatalogName nie może być puste.")
                .NotNull().WithMessage("Pole CatalogName nie może przyjmować null.");

            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");
        }
    }
}
