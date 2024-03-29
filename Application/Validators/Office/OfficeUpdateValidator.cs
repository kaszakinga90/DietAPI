﻿using Application.DTOs.OfficeDTO;
using FluentValidation;

namespace Application.Validators.Office
{
    public class OfficeUpdateValidator : AbstractValidator<OfficeEditDTO>
    {
        public OfficeUpdateValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Pole UserId nie może być puste.")
                .NotNull().WithMessage("Pole UserId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole UserId musi być liczbą całkowitą większą niż 0.");

            RuleFor(dto => dto.OfficeName)
                .NotEmpty().WithMessage("Pole OfficeName nie może być puste.")
                .NotNull().WithMessage("Pole OfficeName nie może przyjmować null.")
                .MinimumLength(3).WithMessage("Minimalna długość OfficeName to 3.");
        }
    }
}