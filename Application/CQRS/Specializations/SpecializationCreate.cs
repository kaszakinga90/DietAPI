using Application.Core;
using Application.DTOs.SpecializationDTO;
using Application.Validators.Specialization;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Specializations
{
    public class SpecializationCreate
    {
        public class Command : IRequest<Result<SpecializationPostDTO>>
        {
            public SpecializationPostDTO SpecializationPostDTO { get; set; }
        }
        public class Hendler : IRequestHandler<Command, Result<SpecializationPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly SpecializationCreateValidator _validator;

            public Hendler(DietContext context, IMapper mapper, SpecializationCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<SpecializationPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.SpecializationPostDTO, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<SpecializationPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var specialization = _mapper.Map<Specialization>(request.SpecializationPostDTO);

                if (specialization == null)
                {
                    return Result<SpecializationPostDTO>.Failure("Niepowodzenie mapowania.");
                }

                _context.SpecializationsDb.Add(specialization);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<SpecializationPostDTO>.Failure("Operacja nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<SpecializationPostDTO>.Failure("Wystąpił błąd podczas dodawania specjalizacji.");
                }

                return Result<SpecializationPostDTO>.Success(_mapper.Map<SpecializationPostDTO>(specialization));
            }
        }
    }
}