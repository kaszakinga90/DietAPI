using Application.DTOs.TestsResultsDTO;
using FluentValidation;

namespace Application.Validators.TestResults
{
    public class TestResultCreateValidator : AbstractValidator<TestResultPostDTO>
    {
        public TestResultCreateValidator()
        {
            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.PatientCardId)
                .NotEmpty().WithMessage("Pole PatientCardId nie może być puste.")
                .NotNull().WithMessage("Pole PatientCardId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole PatientCardId musi być liczbą całkowitą większą niż 0.");
        }
    }
}