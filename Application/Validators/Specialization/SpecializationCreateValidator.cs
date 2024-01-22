using Application.DTOs.SpecializationDTO;
using FluentValidation;

namespace Application.Validators.Specialization
{
    public class SpecializationCreateValidator : AbstractValidator<SpecializationPostDTO>
    {
        public SpecializationCreateValidator()
        {
            RuleFor(dto => dto.SpecializationName)
                .NotEmpty().WithMessage("Pole SpecializationName nie może być puste.")
                .NotNull().WithMessage("Pole SpecializationName nie może przyjmować null.");
        }
    }
}