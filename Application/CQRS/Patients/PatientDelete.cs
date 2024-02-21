using Application.Core;
using Application.DTOs.PatientDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using System.Diagnostics;

namespace Application.CQRS.Patients
{
    public class PatientDelete
    {
        public class Command : IRequest<Result<PatientDeleteDTO>>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<PatientDeleteDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<PatientDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var patient = await _context.PatientsDb
                    .Include(d => d.Address)
                    .FirstOrDefaultAsync(i => i.Id == request.Id);

                if (patient == null)
                {
                    return Result<PatientDeleteDTO>.Failure("Pacjent o podanym ID nie został znaleziony.");
                }

                var patientDTO = _mapper.Map<PatientDeleteDTO>(patient);

                if (patientDTO.isActive == false)
                {
                    return Result<PatientDeleteDTO>.Failure("Pacjent ma już status USUNIĘTY");
                }
                else
                {
                    patientDTO.isActive = false;

                    if (patientDTO.AddressDeleteDTO != null)
                    {
                        patientDTO.AddressDeleteDTO.isActive = false;
                    }

                    _mapper.Map(patientDTO, patient);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                        if (!result)
                        {
                            return Result<PatientDeleteDTO>.Failure("Usunięcie pacjenta nie powiodło się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Błąd podczas usuwania pacjenta: " + ex);
                        return Result<PatientDeleteDTO>.Failure("Wystąpił błąd podczas usuwania pacjenta.");
                    }

                    return Result<PatientDeleteDTO>.Success(_mapper.Map<PatientDeleteDTO>(patient));
                }
            }
        }
    }
}
