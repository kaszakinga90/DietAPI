using Application.DTOs.DishDetailsToEditDTO;
using FluentValidation;

namespace Application.Validators.DishEditDetails
{
    public class DishBaseDetailsUpdateValidator : AbstractValidator<DishDetailsGetEditDTO>
    {
        public DishBaseDetailsUpdateValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Pole Name nie może być puste.")
                .NotNull().WithMessage("Pole Name nie może przyjmować null.")
                .MinimumLength(3).WithMessage("Minimalna długość Name to 3.")
                .When(dto => dto.Name != null);

            RuleFor(dto => dto.PreparingTime)
                .NotEmpty().WithMessage("Pole PreparingTime nie może być puste.")
                .NotNull().WithMessage("Pole PreparingTime nie może przyjmować null.")
                .When(dto => dto.PreparingTime != null);

            RuleFor(dto => dto.UnitId)
                .Null().When(d => d == null)
                .NotEmpty().WithMessage("Pole UnitId nie może być puste.")
                .NotNull().WithMessage("Pole UnitId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole UnitId musi być liczbą całkowitą większą niż 0.")
                .When(dto => dto.UnitId != null);

            RuleFor(dto => dto.ServingQuantity)
                .Null().When(d => d == null)
                    .WithMessage("Pole ServingQuantity nie może mieć wartości, gdy jest puste.")
                .GreaterThan(0).When(d => d != null)
                    .WithMessage("Wartość pola ServingQuantity musi być większa niż 0.")
                 .When(dto => dto.ServingQuantity != null);

            RuleFor(dto => dto.MeasureId)
                .Null().When(measureId => measureId == null)
                    .WithMessage("Pole MeasureId nie może mieć wartości, gdy jest puste.")
                .GreaterThan(0).When(measureId => measureId != null)
                    .WithMessage("Wartość pola MeasureId musi być większa niż 0.")
                .When(dto => dto.MeasureId != null);
        }
    }
}