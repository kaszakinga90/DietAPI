using Application.DTOs.RoleDTO;
using FluentValidation;

namespace Application.Validators.Role
{
    public class RoleCreateValidator : AbstractValidator<RolePostDTO>
    {
        public RoleCreateValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Pole Name nie może być puste.")
                .NotNull().WithMessage("Pole Name nie może przyjmować null.");
        }
    }
}