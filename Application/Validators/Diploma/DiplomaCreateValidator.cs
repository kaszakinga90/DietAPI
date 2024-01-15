using Application.DTOs.DiplomaDTO;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Validators.Diploma
{
    public class DiplomaCreateValidator : AbstractValidator<DiplomaPostDTO>
    {
        public DiplomaCreateValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage("Pole Title nie może być puste.")
                .NotNull().WithMessage("Pole Title nie może przyjmować null.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Pole Description nie może być puste.")
                .NotNull().WithMessage("Pole Description nie może przyjmować null.");

            RuleFor(dto => dto.DieticianId)
                .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
                .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
                .GreaterThan(1).WithMessage("Pole DieticianId musi być liczbą całkowitą większą niż 1.");

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