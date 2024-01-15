using FluentValidation;

// TODO : przerobić żeby działałąo na DTO
namespace Application.Validators.Patient
{
    public class PatientCreateValidator : AbstractValidator<ModelsDB.Patient>
    {
        public PatientCreateValidator()
        {
            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage("Pole Email nie może być puste.")
                .EmailAddress().WithMessage("Nieprawidłowy format adresu e-mail.");

            //RuleFor(dto => dto.Password)
            //    .NotEmpty().WithMessage("Pole Password nie może być puste.");

            RuleFor(dto => dto.PictureUrl)
                .NotEmpty().WithMessage("Pole PhotoUrl nie może być puste.");
        }
    }
}

// TODO : walidacja dla hasła do uzupełnienia