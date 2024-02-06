using Application.DTOs.InvitationDTO;
using FluentValidation;

namespace Application.Validators.Invitation
{
    public class InvitationCreateValidator : AbstractValidator<InvitationPostDTO>
    {
        public InvitationCreateValidator()
        {
            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.PatientId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.IsSended)
                .Equal(true).WithMessage("Pole IsSended musi mieć wartość true.");
        }
    }
}