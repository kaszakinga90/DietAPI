using Application.DTOs.OfficeDTO;
using FluentValidation;

namespace Application.Validators.Office
{
    public class OfficeCreateValidator : AbstractValidator<OfficePostDTO>
    {
        public OfficeCreateValidator()
        {
            //RuleFor(dto => dto.OfficeName)
            //    .NotEmpty().WithMessage("Pole OfficeName nie może być puste.")
            //    .NotNull().WithMessage("Pole OfficeName nie może przyjmować null.")
            //    .MinimumLength(3).WithMessage("Minimalna długość OfficeName to 3.");
        }
    }
}