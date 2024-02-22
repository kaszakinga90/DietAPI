using Application.DTOs.DieticianDTO;
using FluentValidation;

namespace Application.Validators.Dietician
{
    public class DieticianUpdateDataValidator : AbstractValidator<DieticianEditDataDTO>
    {
        public DieticianUpdateDataValidator()
        {
            RuleFor(dto => dto.FirstName)
                .NotEmpty().When(dto => dto.FirstName != null).WithMessage("Pole FirstName nie może być puste.");

            RuleFor(dto => dto.LastName)
                .NotEmpty().When(dto => dto.FirstName != null).WithMessage("Pole LastName nie może być puste.");

            RuleFor(dto => dto.Email)
                .NotNull().WithMessage("Pole Email nie może być null.")
                .NotEmpty().WithMessage("Pole Email nie może być puste.")
                .EmailAddress().WithMessage("Nieprawidłowy format adresu e-mail.");

            RuleFor(dto => dto.PhoneNumber)
                .MaximumLength(20).When(dto => dto.PhoneNumber != null).WithMessage("Pole PhoneNumber nie może przekraczać 20 znaków.");

            RuleFor(dto => dto.BirthDate)
                .Must(date => date.HasValue && date.Value.Year > 1900).When(dto => dto.BirthDate != null).WithMessage("Nieprawidłowa data urodzenia.");
        }
    }
}