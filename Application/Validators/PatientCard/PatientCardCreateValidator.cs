using Application.DTOs.PatientCardDTO;
using FluentValidation;

namespace Application.Validators.PatientCard
{
    public class PatientCardCreateValidator : AbstractValidator<PatientCardPostDTO>
    {
        public PatientCardCreateValidator()
        {
            RuleFor(dto => dto.PatientId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.SexId)
                .NotEmpty().WithMessage("Pole SexId nie może być puste.")
                .NotNull().WithMessage("Pole SexId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole SexId musi być liczbą całkowitą większą niż 0.");
        }
    }
}