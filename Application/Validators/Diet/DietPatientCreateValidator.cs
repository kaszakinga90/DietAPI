using Application.DTOs.DietPatientDTO;
using FluentValidation;

namespace Application.Validators.Diet
{
    public class DietPatientCreateValidator : AbstractValidator<DietPatientPostDTO>
    {
        public DietPatientCreateValidator()
        {
            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.PatientId)
                .NotEmpty().WithMessage("Pole PatientId nie może być puste.")
                .NotNull().WithMessage("Pole PatientId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole PatientId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Pole Id nie może być puste.")
                .NotNull().WithMessage("Pole Id nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole Id musi być liczbą całkowitą większą niż 0.");
        }
    }
}