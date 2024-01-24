using Application.DTOs.DishDTO;
using FluentValidation;

namespace Application.Validators.Dish
{
    public class DishUpdateValidator : AbstractValidator<DishEditDTO>
    {
        public DishUpdateValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Pole Name nie może być puste.")
                .NotNull().WithMessage("Pole Name nie może przyjmować null.")
                .MinimumLength(3).WithMessage("Minimalna długość Name to 3.");

            //RuleFor(dto => dto.DieteticianId)
            //    .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
            //    .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
            //    .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.UnitId)
                .NotEmpty().WithMessage("Pole UnitId nie może być puste.")
                .NotNull().WithMessage("Pole UnitId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole UnitId musi być liczbą całkowitą większą niż 0.");

            RuleFor(dto => dto.ServingQuantity)
                .Null().When(sq => sq == null)
                    .WithMessage("Pole ServingQuantity nie może mieć wartości, gdy jest puste.")
                .GreaterThan(0).When(sq => sq != null)
                    .WithMessage("Wartość pola ServingQuantity musi być większa niż 0.");

            RuleFor(dto => dto.MeasureId)
                .Null().When(measureId => measureId == null)
                    .WithMessage("Pole MeasureId nie może mieć wartości, gdy jest puste.")
                .GreaterThan(0).When(measureId => measureId != null)
                    .WithMessage("Wartość pola MeasureId musi być większa niż 0.");
        }
    }
}
