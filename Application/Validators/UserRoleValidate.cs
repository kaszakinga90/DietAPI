using Application.DTOs.UsersDTO.UserRoleDTO;
using FluentValidation;

namespace Application.Validators
{
    public class UserRoleValidate : AbstractValidator<UserRoleCreateDTO>
    {
        public UserRoleValidate()
        {
            RuleFor(dto => dto.RoleId)
                .NotEmpty().WithMessage("Pole RoleId nie może być puste.")
                .GreaterThan(1).WithMessage("Wartość RoleId musi być większa niż 1.");
        }
    }
}
