using Application.Core;
using Application.DTOs.DieticianSpecializationsDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Specializations
{
    public class DieticianSpecializationDelete
    {
        public class Command : IRequest<Result<DieticianSpecializationDeleteDTO>>
        {
            public DieticianSpecializationDeleteDTO DieticianSpecializationDeleteDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<DieticianSpecializationDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<DieticianSpecializationDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var dieticianSpecialization = await _context.DieticianSpecialization
                        .SingleOrDefaultAsync(di => di.DieticianId == request.DieticianSpecializationDeleteDTO.DieticianId 
                                            && di.SpecializationId == request.DieticianSpecializationDeleteDTO.SpecializationId, cancellationToken);

                    if (dieticianSpecialization == null)
                    {
                        return Result<DieticianSpecializationDeleteDTO>.Failure("Dietician speccialization not found.");
                    }

                    _mapper.Map(request.DieticianSpecializationDeleteDTO, dieticianSpecialization);

                    _context.DieticianSpecialization.Remove(dieticianSpecialization);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<DieticianSpecializationDeleteDTO>.Failure("Operacja nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<DieticianSpecializationDeleteDTO>.Failure("Wystąpił błąd podczas usuwania test results.");
                    }

                    return Result<DieticianSpecializationDeleteDTO>.Success(_mapper.Map<DieticianSpecializationDeleteDTO>(dieticianSpecialization));
                }
            }
        }
    }
}