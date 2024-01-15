using Application.DTOs.PatientDTO;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Validators.Patient
{
    public class PatientUpdateValidator : AbstractValidator<PatientEditDTO>
    {
        public PatientUpdateValidator()
        {
            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage("Pole Email nie może być puste.")
                .EmailAddress().WithMessage("Nieprawidłowy format adresu e-mail.");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Pole Password nie może być puste.");

            RuleFor(dto => dto.PhotoUrl)
                .NotEmpty().WithMessage("Pole PhotoUrl nie może być puste.");

            //RuleFor(dto => dto.FirstName)
            //    .NotNull().WithMessage("Pole FirstName nie może być null.")
            //    .NotEmpty().WithMessage("Pole FirstName nie może być puste.");

            //RuleFor(dto => dto.LastName)
            //    .NotNull().WithMessage("Pole LastName nie może być null.")
            //    .NotEmpty().WithMessage("Pole LastName nie może być puste.");

            //RuleFor(dto => dto.PhoneNumber)
            //    .NotNull().WithMessage("Pole PhoneNumber nie może być null.")
            //    .NotEmpty().WithMessage("Pole PhoneNumber nie może być puste.")
            //    .MaximumLength(20).WithMessage("Pole PhoneNumber nie może przekraczać 20 znaków.");

            //RuleFor(dto => dto.BirthDate)
            //    .Must(date => date.HasValue && date.Value.Year > 1900).WithMessage("Nieprawidłowa data urodzenia.");

            RuleFor(dto => dto.File)
                .NotNull().WithMessage("Pole File nie może być null.")
                .Must(isFileHasValidExtension).WithMessage("Plik musi być w formacie JPG lub PNG.");
        }

        private bool isFileHasValidExtension(IFormFile file)
        {
            if (file == null)
                return false;

            var validExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var uploadFileExtension = Path.GetExtension(file.FileName)?.ToLowerInvariant();

            return validExtensions.Contains(uploadFileExtension);
        }
    }
}

// TODO : walidacja dla hasła do uzupełnienia