using Application.DTOs.FoodCatalogDTO;
using FluentValidation;

namespace Application.Validators.FoodCatalog
{
    public class FoodCatalogDieticianCreateValidator : AbstractValidator<FoodCatalogPostDTO>
    {
        public FoodCatalogDieticianCreateValidator()
        {
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