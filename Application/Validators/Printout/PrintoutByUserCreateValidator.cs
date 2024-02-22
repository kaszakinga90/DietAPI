using Application.DTOs.PrintoutsDTO;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Validators.Printout
{
    public class PrintoutByUserCreateValidator : AbstractValidator<PrintoutUploadByUserPostDTO>
    {
        public PrintoutByUserCreateValidator()
        {
            RuleFor(dto => dto.DieticianId)
               .NotEmpty().WithMessage("Pole DieticianId nie może być puste.")
               .NotNull().WithMessage("Pole DieticianId nie może przyjmować null.")
               .GreaterThan(0).WithMessage("Pole DieticianId musi mieć wartość większą niż 0");

            RuleFor(dto => dto.TemplateFile)
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
