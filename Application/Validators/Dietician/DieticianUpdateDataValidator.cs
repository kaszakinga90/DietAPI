﻿using Application.DTOs.DieticianDTO;
using FluentValidation;

namespace Application.Validators.Dietician
{
    public class DieticianUpdateDataValidator : AbstractValidator<DieticianEditDataDTO>
    {
        public DieticianUpdateDataValidator()
        {
            //RuleFor(dto => dto.Email)
            //    .NotEmpty().WithMessage("Pole Email nie może być puste.")
            //    .EmailAddress().WithMessage("Nieprawidłowy format adresu e-mail.");

            //RuleFor(dto => dto.Password)
            //    .NotEmpty().WithMessage("Pole Password nie może być puste.");

            RuleFor(dto => dto.FirstName)
                .NotNull().WithMessage("Pole FirstName nie może być null.")
                .NotEmpty().WithMessage("Pole FirstName nie może być puste.");

            RuleFor(dto => dto.LastName)
                .NotNull().WithMessage("Pole LastName nie może być null.")
                .NotEmpty().WithMessage("Pole LastName nie może być puste.");

            //RuleFor(dto => dto.PhoneNumber)
            //    .NotNull().WithMessage("Pole PhoneNumber nie może być null.")
            //    .NotEmpty().WithMessage("Pole PhoneNumber nie może być puste.")
            //    .MaximumLength(20).WithMessage("Pole PhoneNumber nie może przekraczać 20 znaków.");

            //RuleFor(dto => dto.BirthDate)
            //    .Must(date => date.HasValue && date.Value.Year > 1900).WithMessage("Nieprawidłowa data urodzenia.");
        }
    }
}

// TODO : walidacja dla hasła do uzupełnienia