using Application.DTOs.FoodCatalogDTO;
using FluentValidation;

namespace Application.Validators.FoodCatalog
{
    public class FoodCatalogForAdminCreateValidator : AbstractValidator<FoodCatalogPostDTO>
    {
        public FoodCatalogForAdminCreateValidator()
        {
            RuleFor(dto => dto.CatalogName)
                .NotEmpty().WithMessage("Pole CatalogName nie może być puste.")
                .NotNull().WithMessage("Pole CatalogName nie może przyjmować null.");
        }
    }
}