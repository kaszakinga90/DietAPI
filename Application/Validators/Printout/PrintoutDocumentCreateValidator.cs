using Application.DTOs.PrintoutsDTO;
using FluentValidation;

namespace Application.Validators.Printout
{
    public class PrintoutDocumentCreateValidator : AbstractValidator<PrintoutDocumentPostDTO>
    {
        public PrintoutDocumentCreateValidator()
        {
            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi mieć wartość większą niż 1");
        }
    }
}