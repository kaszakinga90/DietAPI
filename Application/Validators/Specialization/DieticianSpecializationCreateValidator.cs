using Application.DTOs.SpecializationDTO;
using FluentValidation;

namespace Application.Validators.Specialization
{
    public class DieticianSpecializationCreateValidator : AbstractValidator<DieteticianSpecializationPostDTO>
    {
        public DieticianSpecializationCreateValidator()
        {
            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.SpecializationId)
                .NotEmpty().WithMessage("Pole SpecializationId nie może być puste.")
                .NotNull().WithMessage("Pole SpecializationId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole SpecializationId musi być liczbą całkowitą większą niż 0.");

            RuleFor(dto => dto.SpecializationName)
                .NotEmpty().WithMessage("Pole SpecializationName nie może być puste.")
                .NotNull().WithMessage("Pole SpecializationName nie może przyjmować null.");
        }
    }
}