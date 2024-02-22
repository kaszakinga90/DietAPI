using Application.DTOs.BillDTO;
using FluentValidation;

namespace Application.Validators.Bill
{
    public class BillUpdateValidator : AbstractValidator<SalesPutDTO>
    {
        public BillUpdateValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Pole Id nie może być puste.")
                .NotNull().WithMessage("Pole Id nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole Id musi mieć wartość większą niż 0");
        }
    }
}