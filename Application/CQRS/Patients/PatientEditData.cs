using Application.Core;
using Application.DTOs.PatientDTO;
using Application.Validators.Patient;
using AutoMapper;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Patients
{
    public class PatientEditData
    {
        public class Command : IRequest<Result<PatientEditDataDTO>>
        {
            public PatientEditDataDTO PatientEditDataDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<PatientEditDataDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;
                private readonly PatientUpdateDataValidator _validator;

                public Handler(DietContext context, IMapper mapper, PatientUpdateDataValidator validator)
                {
                    _context = context;
                    _mapper = mapper;
                    _validator = validator;
                }

                public async Task<Result<PatientEditDataDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var validationResult = await _validator
                    .ValidateAsync(request.PatientEditDataDTO);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                        return Result<PatientEditDataDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                    }

                    var patient = await _context.PatientsDb
                        .FindAsync(new object[] { request.PatientEditDataDTO.Id });

                    if (patient == null)
                    {
                        return Result<PatientEditDataDTO>.Failure("Pacjent o podanym ID nie został znaleziony.");
                    }

                    _mapper.Map(request.PatientEditDataDTO, patient);

                    try
                    {
                        var result = await _context.SaveChangesAsync() > 0;
                        if (!result)
                        {
                            return Result<PatientEditDataDTO>.Failure("Edycja pacjenta nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<PatientEditDataDTO>.Failure("Wystąpił błąd podczas edycji pacjenta. " + ex);
                    }
                    return Result<PatientEditDataDTO>.Success(_mapper.Map<PatientEditDataDTO>(patient));
                }
            }
        }
    }
}