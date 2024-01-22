using Application.Core;
using Application.DTOs.SpecializationDTO;
using Application.Validators.Specialization;
using AutoMapper;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Specializations
{
    public class SpecializationEdit
    {
        public class Command : IRequest<Result<SpecializationPostDTO>>
        {
            public SpecializationPostDTO SpecializationEditDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<SpecializationPostDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;
                private readonly SpecializationCreateValidator _validator;

                public Handler(DietContext context, IMapper mapper, SpecializationCreateValidator validator)
                {
                    _context = context;
                    _mapper = mapper;
                    _validator = validator;
                }

                public async Task<Result<SpecializationPostDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var validationResult = await _validator
                    .ValidateAsync(request.SpecializationEditDTO, cancellationToken);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                        return Result<SpecializationPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                    }

                    var specialization = await _context.SpecializationsDb
                        .FindAsync(new object[] { request.SpecializationEditDTO.Id }, cancellationToken);

                    if (specialization == null)
                    {
                        return Result<SpecializationPostDTO>.Failure("Pacjent o podanym ID nie został znaleziony.");
                    }

                    _mapper.Map(request.SpecializationEditDTO, specialization);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<SpecializationPostDTO>.Failure("Edycja pacjenta nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<SpecializationPostDTO>.Failure("Wystąpił błąd podczas edycji pacjenta. " + ex);
                    }
                    return Result<SpecializationPostDTO>.Success(_mapper.Map<SpecializationPostDTO>(specialization));
                }
            }
        }
    }
}
