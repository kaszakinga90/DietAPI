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
        }
    }
}