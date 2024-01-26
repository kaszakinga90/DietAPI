using Application.Core;
using Application.DTOs.DietPatientDTO;
using Application.Validators.Diet;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.DietsForPatients.DietPatients
{
    public class DietPatientCreate
    {
        public class Command : IRequest<Result<DietPatientPostDTO>>
        {
            public DietPatientPostDTO DietPatientPostDTO { get; set; }
        }
        public class Hendler : IRequestHandler<Command, Result<DietPatientPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly DietPatientCreateValidator _validator;

            public Hendler(DietContext context, IMapper mapper, DietPatientCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<DietPatientPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.DietPatientPostDTO, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<DietPatientPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var dietPatientDTO = request.DietPatientPostDTO;

                var dietPatient = _mapper.Map<DietPatient>(dietPatientDTO);

                if (dietPatient == null)
                {
                    return Result<DietPatientPostDTO>.Failure("Niepowodzenie mapowania.");
                }

                _context.DietPatientsDb.Add(dietPatient);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<DietPatientPostDTO>.Failure("Operacja nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DietPatientPostDTO>.Failure("Wystąpił błąd podczas dodawania specjalizacji.");
                }

                return Result<DietPatientPostDTO>.Success(dietPatientDTO);
            }
        }
    }
}