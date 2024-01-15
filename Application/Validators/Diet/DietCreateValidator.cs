using FluentValidation;

namespace Application.Validators.Diet
{
    public class DietCreateValidator : AbstractValidator<DietPostDTO>
    {
        public DietCreateValidator()
        {
            RuleFor(dto => dto.DieteticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.PatientId)
                .NotEmpty().WithMessage("Pole PatientId nie może być puste.")
                .NotNull().WithMessage("Pole PatientId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole PatientId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.Name)
            .NotNull().WithMessage("Pole Name nie może być null.")
            .NotEmpty().WithMessage("Pole Name nie może być puste.");

            RuleFor(dto => dto.StartDate)
                .NotNull().WithMessage("Pole StartDate nie może być null.")
                .NotEmpty().WithMessage("Pole StartDate nie może być puste.");

            RuleFor(dto => dto.EndDate)
                .NotNull().WithMessage("Pole EndDate nie może być null.")
                .NotEmpty().WithMessage("Pole EndDate nie może być puste.")
                .GreaterThanOrEqualTo(dto => dto.StartDate).WithMessage("EndDate musi być większe lub równe StartDate.");

            RuleFor(dto => dto.numberOfMeals)
                .NotNull().WithMessage("Pole numberOfMeals nie może być null.")
                .GreaterThanOrEqualTo(1).WithMessage("Pole numberOfMeals musi być co najmniej 1.");
        }
    }
}