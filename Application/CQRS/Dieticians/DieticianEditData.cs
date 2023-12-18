using Application.Core;
using Application.DTOs.DieticianDTO;
using AutoMapper;
using DietDB;
using MediatR;

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

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<DieticianEditDataDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
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
                        return Result<DieticianEditDataDTO>.Failure("Wystąpił błąd podczas edycji dietetyka.");
                    }

                    return Result<DieticianEditDataDTO>.Success(_mapper.Map<DieticianEditDataDTO>(dietician));
                }
            }
        }
    }
}