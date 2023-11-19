using Application.Core;
using Application.DTOs.MealScheduleDTO;
using Application.DTOs.PatientDTO;
using AutoMapper;
using DietDB;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Patients
{
    public class PatientEditData
    {
        public class Command : IRequest<Result<PatientEditDataDTO>>
        {
            public PatientEditDataDTO PatientEditData { get; set; }
            public class Handler : IRequestHandler<Command, Result<PatientEditDataDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<PatientEditDataDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var patient = await _context.PatientsDb.FindAsync(new object[] { request.PatientEditData.Id }, cancellationToken);
                    if (patient == null)
                    {
                        return Result<PatientEditDataDTO>.Failure("Pacjent o podanym ID nie został znaleziony.");
                    }

                    _mapper.Map(request.PatientEditData, patient);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<PatientEditDataDTO>.Failure("Edycja pacjenta nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        return Result<PatientEditDataDTO>.Failure("Wystąpił błąd podczas edycji pacjenta.");
                    }

                    return Result<PatientEditDataDTO>.Success(_mapper.Map<PatientEditDataDTO>(patient));
                }
            }
        }
    }
}