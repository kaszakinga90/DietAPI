using Application.DTOs.UsersDTO.UserRoleDTO;
using FluentValidation;

namespace Application.Validators.UserRole
{
    public class UserRoleCreateValidate : AbstractValidator<UserRoleCreateDTO>
    {
        public UserRoleCreateValidate()
        {
            RuleFor(dto => dto.UserId)
                .NotEmpty().WithMessage("Pole UserId nie może być puste.")
                .NotNull().WithMessage("Pole UserId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Wartość UserId musi być większa niż 1.");

            RuleFor(dto => dto.RoleId)
                .NotEmpty().WithMessage("Pole RoleId nie może być puste.")
                .NotNull().WithMessage("Pole RoleId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Wartość RoleId musi być większa niż 1.");

            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Pole Name nie może być puste.")
                .NotNull().WithMessage("Pole Name nie może przyjmować null.")
                .MinimumLength(5).WithMessage("Minimalna długość Name to 5.");
        }
    }
}