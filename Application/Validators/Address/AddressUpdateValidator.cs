using Application.DTOs.AddressDTO;
using Application.DTOs.AdminDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Address
{
    public class AddressUpdateValidator : AbstractValidator<AddressPostDTO>
    {
        public AddressUpdateValidator()
        {
            //RuleFor(dto => dto.CountryStateId)
            //     .GreaterThan(0).WithMessage("Wybierz stan.")
            //     .When(dto => dto.CountryStateId != 0) ; // Reguła zostanie sprawdzona tylko, gdy CountryStateId ma wartość

            //RuleFor(dto => dto.ZipCode)
            //    .NotEmpty().WithMessage("Pole ZipCode nie może być puste.")
            //    .When(dto => !string.IsNullOrWhiteSpace(dto.ZipCode)); // Reguła zostanie sprawdzona tylko, gdy ZipCode nie jest puste ani null
        }
    }
}
