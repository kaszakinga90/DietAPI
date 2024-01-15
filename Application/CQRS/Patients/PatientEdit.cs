using Application.Core;
using Application.DTOs.PatientDTO;
using Application.Services;
using Application.Validators.Patient;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Application.CQRS.Patients
{
    public class PatientEdit
    {
        public class Command : IRequest<Result<PatientEditDTO>>
        {
            public PatientEditDTO PatientEditDTO { get; set; }
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<PatientEditDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;
            private readonly PatientUpdateValidator _validator;

            public Handler(DietContext context, IMapper mapper, ImageService imageService, PatientUpdateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
                _validator = validator;
            }

            public async Task<Result<PatientEditDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.PatientEditDTO, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<PatientEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var patient = await _context.PatientsDb.FindAsync(new object[] { request.PatientEditDTO.Id }, cancellationToken);
                if (patient == null)
                {
                    return Result<PatientEditDTO>.Failure("Pacjent o podanym ID nie został znaleziony.");
                }

                _mapper.Map(request.PatientEditDTO, patient);

                // Obsługa obrazu
                if (request.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(request.File);
                    if (imageResult.Error != null)
                    {
                        return Result<PatientEditDTO>.Failure(imageResult.Error.Message);
                    }

                    if (!string.IsNullOrEmpty(patient.PublicId))
                    {
                        await _imageService.DeleteImageAsync(patient.PublicId);
                    }

                    patient.PictureUrl = imageResult.SecureUrl.ToString();
                    patient.PublicId = imageResult.PublicId;
                }

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<PatientEditDTO>.Failure("Edycja pacjenta nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PatientEditDTO>.Failure("Wystąpił błąd podczas edycji pacjenta. " + ex);
                }

                return Result<PatientEditDTO>.Success(_mapper.Map<PatientEditDTO>(patient));
            }
        }
    }
}
