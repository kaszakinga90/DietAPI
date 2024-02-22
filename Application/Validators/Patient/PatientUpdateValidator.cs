using Application.DTOs.PatientDTO;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Validators.Patient
{
    public class PatientUpdateValidator : AbstractValidator<PatientEditDTO>
    {
        public PatientUpdateValidator()
        {
            RuleFor(dto => dto.File)
                 .Must(isFileHasValidExtension).When(dto => dto.File != null).WithMessage("Plik musi być w formacie JPG. JPEG lub PNG.");
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