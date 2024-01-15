using Application.Core;
using Application.DTOs.DieticianDTO;
using Application.Validators.Dietician;
using AutoMapper;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Dieticians
{
    public class DieticianEditData
    {
        public class Command : IRequest<Result<DieticianEditDataDTO>>
        {
            public DieticianEditDataDTO DieticianEditData { get; set; }
            public class Handler : IRequestHandler<Command, Result<DieticianEditDataDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;
                private readonly DieticianUpdateDataValidator _validator;

                public Handler(DietContext context, IMapper mapper, DieticianUpdateDataValidator validator)
                {
                    _context = context;
                    _mapper = mapper;
                    _validator = validator;
                }

                public async Task<Result<DieticianEditDataDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var validationResult = await _validator
                        .ValidateAsync(request.DieticianEditData, cancellationToken);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                        return Result<DieticianEditDataDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                    }

                    var dietician = await _context.DieticiansDb.FindAsync(new object[] { request.DieticianEditData.Id }, cancellationToken);
                    if (dietician == null)
                    {
                        return Result<DieticianEditDataDTO>.Failure("Dietetyk o podanym ID nie został znaleziony.");
                    }

                    _mapper.Map(request.DieticianEditData, dietician);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<DieticianEditDataDTO>.Failure("Edycja dietetyka nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<DieticianEditDataDTO>.Failure("Wystąpił błąd podczas edycji dietetyka. " + ex);
                    }

                    return Result<DieticianEditDataDTO>.Success(_mapper.Map<DieticianEditDataDTO>(dietician));
                }
            }
        }
    }
}