using Application.DTOs.BillDTO;
using FluentValidation;

namespace Application.Validators.Bill
{
    public class BillCreateValidator : AbstractValidator<DietSalesBillPostDTO>
    {
        public BillCreateValidator()
        {
            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole DieticianId musi mieć wartość większą niż 0");

            RuleFor(dto => dto.PatientId)
                .NotEmpty().WithMessage("Pole PatientId nie może być puste.")
                .NotNull().WithMessage("Pole PatientId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole PatientId musi mieć wartość większą niż 0");

            RuleFor(dto => dto.Sales.Price)
                .NotEmpty().WithMessage("Pole Price nie może być puste.")
                .NotNull().WithMessage("Pole Price nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole Kwota musi mieć wartość większą niż 0");
        }
    }
}