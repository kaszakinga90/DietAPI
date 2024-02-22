using Application.DTOs.LogoDTO;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Validators.Logo
{
    public class LogoCreateValidator : AbstractValidator<LogoPostDTO>
    {
        public LogoCreateValidator()
        {
            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.File)
                .NotNull().WithMessage("Pole File nie może być null.")
                .Must(isFileHasValidExtension).WithMessage("Plik musi być w formacie JPG, JPEG lub PNG.");
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