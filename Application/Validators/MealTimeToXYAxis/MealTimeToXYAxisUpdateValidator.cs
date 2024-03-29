﻿using Application.DTOs.MealTimeToXYAxisDTO;
using FluentValidation;

namespace Application.Validators.MealTimeToXYAxis
{
    public class MealTimeToXYAxisUpdateValidator : AbstractValidator<MealTimeToXYAxisEditDTO>
    {
        public MealTimeToXYAxisUpdateValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Pole UserId nie może być puste.")
                .NotNull().WithMessage("Pole UserId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole UserId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.DietId)
                .NotEmpty().WithMessage("Pole DietId nie może być puste.")
                .NotNull().WithMessage("Pole DietId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole DietId musi być liczbą całkowitą większą niż 1.");

            RuleFor(dto => dto.DishId)
                .NotEmpty().WithMessage("Pole DishId nie może być puste.")
                .NotNull().WithMessage("Pole DishId nie może przyjmować null.")
                .GreaterThan(0).WithMessage("Pole DishId musi być liczbą całkowitą większą niż 1.");
        }
    }
}