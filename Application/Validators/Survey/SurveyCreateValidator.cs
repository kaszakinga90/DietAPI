using Application.DTOs.Surveys;
using FluentValidation;

namespace Application.Validators.Survey
{
    public class SurveyCreateValidator : AbstractValidator<SurveyPostDTO>
    {
        public SurveyCreateValidator()
        {
            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.Weith)
                .NotEmpty().WithMessage("Pole Weith nie może być puste.")
                .NotNull().WithMessage("Pole Weith nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole Weith musi być liczbą całkowitą większą niż 0.");

            RuleFor(dto => dto.Heigth)
                .NotEmpty().WithMessage("Pole Heigth nie może być puste.")
                .NotNull().WithMessage("Pole Heigth nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole Heigth musi być liczbą całkowitą większą niż 0.");

            RuleFor(dto => dto.PatientCardId)
                .NotEmpty().WithMessage("Pole PatientCardId nie może być puste.")
                .NotNull().WithMessage("Pole PatientCardId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole PatientCardId musi być liczbą całkowitą większą niż 0.");
        }
    }
}