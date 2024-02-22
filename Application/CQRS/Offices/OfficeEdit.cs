using Application.Core;
using Application.DTOs.OfficeDTO;
using Application.Validators.Office;
using AutoMapper;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Offices
{
    public class OfficeEdit
    {
        public class Command : IRequest<Result<OfficeEditDTO>>
        {
            public OfficeEditDTO OfficeEditDTO { get; set; }
            public class Handler : IRequestHandler<Command, Result<OfficeEditDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;
                private readonly OfficeUpdateValidator _validator;

                public Handler(DietContext context, IMapper mapper, OfficeUpdateValidator validator)
                {
                    _context = context;
                    _mapper = mapper;
                    _validator = validator;
                }

                public async Task<Result<OfficeEditDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var validationResult = await _validator
                    .ValidateAsync(request.OfficeEditDTO);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                        return Result<OfficeEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                    }

                    var office = await _context.OfficesDb
                        .FindAsync(new object[] { request.OfficeEditDTO.Id });
                    
                    if (office == null)
                    {
                        return Result<OfficeEditDTO>.Failure("Biuro o podanym ID nie zostało znalezione.");
                    }

                    _mapper.Map(request.OfficeEditDTO, office);

                    try
                    {
                        var result = await _context.SaveChangesAsync() > 0;
                        if (!result)
                        {
                            return Result<OfficeEditDTO>.Failure("Edycja biura nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<OfficeEditDTO>.Failure("Wystąpił błąd podczas edycji biura. " + ex);
                    }

                    return Result<OfficeEditDTO>.Success(_mapper.Map<OfficeEditDTO>(office));
                }
            }
        }
    }
}