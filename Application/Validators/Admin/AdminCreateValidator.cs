using Application.DTOs.AdminDTO;
using FluentValidation;
using ModelsDB;

namespace Application.Validators.Admin
{
    public class AdminCreateValidator : AbstractValidator<AdminPostDTO>
    {
        public AdminCreateValidator()
        {
            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage("Pole Email nie może być puste.")
                .EmailAddress().WithMessage("Nieprawidłowy format adresu e-mail.");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Pole Password nie może być puste.");
        }
    }
}