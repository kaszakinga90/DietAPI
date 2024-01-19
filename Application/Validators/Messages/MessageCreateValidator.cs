using FluentValidation;

namespace Application.Validators.Messages
{
    public class MessageCreateValidator : AbstractValidator<MessageToDTO>
    {

        public MessageCreateValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage("Pole Title nie może być puste.")
                .NotNull().WithMessage("Pole Title nie może przyjmować null.")
                .MinimumLength(5).WithMessage("Minimalna długość Title to 5.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Pole Description nie może być puste.")
                .NotNull().WithMessage("Pole Description nie może przyjmować null.")
                .MinimumLength(5).WithMessage("Minimalna długość Description to 5.");

            RuleFor(dto => dto.IsRead)
                .Equal(false).WithMessage("Pole IsRead musi mieć wartość false.");

            RuleFor(dto => dto.AdminId)
                .Null().When(adminId => adminId == null)
                    .WithMessage("Pole AdminId nie może mieć wartości, gdy jest puste.")
                .GreaterThan(1).When(adminId => adminId != null)
                    .WithMessage("Wartość pola AdminId musi być większa niż 1.");

            RuleFor(dto => dto.DieticianId)
                .Null().When(dieticianId => dieticianId == null)
                    .WithMessage("Pole DieticianId nie może mieć wartości, gdy jest puste.")
                .GreaterThan(1).When(dieticianId => dieticianId != null)
                    .WithMessage("Wartość pola DieticianId musi być większa niż 1.");

            RuleFor(dto => dto.PatientId)
                .Null().When(patientId => patientId == null)
                    .WithMessage("Pole PatientId nie może mieć wartości, gdy jest puste.")
                .GreaterThan(1).When(patientId => patientId != null)
                    .WithMessage("Wartość pola PatientId musi być większa niż 1.");
        }
    }
}