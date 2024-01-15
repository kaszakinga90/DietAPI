using Application.Validators.Patient;
using DietDB;
using MediatR;
using ModelsDB;

// TODO : do usuni ecia
namespace Application.CQRS.Patients
{
    public class PatientCreate
    {
        public class Command : IRequest
        {
            public Patient Patient { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly PatientCreateValidator _validator;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                //var validationResult = await _validator
                //    .ValidateAsync(request.PatientEditDTO, cancellationToken);

                //if (!validationResult.IsValid)
                //{
                //    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                //    return Result<PatientEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                //}

                _context.PatientsDb.Add(request.Patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}