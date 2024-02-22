using Application.DTOs.PrintoutsDTO;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Validators.Printout
{
    public class PrintoutCreateValidator : AbstractValidator<ParameterizedPrintoutPostDTO>
    {
        public PrintoutCreateValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Pole SpecializationName nie może być puste.")
                .NotNull().WithMessage("Pole SpecializationName nie może przyjmować null.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Pole SpecializationName nie może być puste.")
                .NotNull().WithMessage("Pole SpecializationName nie może przyjmować null.");

            RuleFor(dto => dto.WordFile)
                 .Must(isFileHasValidExtension).WithMessage("Plik musi być w formacie doc, docx lub txt.");
        }

        private bool isFileHasValidExtension(IFormFile file)
        {
            if (file == null)
                return false;

            var validExtensions = new[] { ".doc", ".docx", ".txt" };
            var uploadFileExtension = Path.GetExtension(file.FileName)?.ToLowerInvariant();

            return validExtensions.Contains(uploadFileExtension);
        }
    }
}