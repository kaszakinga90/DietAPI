using FluentValidation;

namespace Application.Validators.Dietician
{
    public class DieticianCreateValidator : AbstractValidator<ModelsDB.Dietician>
    {
        public DieticianCreateValidator()
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

// TODO : przy tworzeniu dietetyka powinien od razu  Logo(przykładowy opbrazek)