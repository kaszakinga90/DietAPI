using Application.Core;
using Application.DTOs.SpecializationDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Specializations
{
    public class SpecializationDelete
    {
        public class Command : IRequest<Result<SpecializationDeleteDTO>>
        {
            public int SpecializationId { get; set; }
            public SpecializationDeleteDTO SpecializationDeleteDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<SpecializationDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<SpecializationDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var specialization = await _context.SpecializationsDb
                        .Include(s => s.DieticianSpecializations)
                        .SingleOrDefaultAsync(di => di.Id == request.SpecializationId && di.isActive, cancellationToken);

                    if (specialization == null)
                    {
                        return Result<SpecializationDeleteDTO>.Failure("Specialization not found.");
                    }

                    if(specialization.DieticianSpecializations.Any())
                    {
                        return Result<SpecializationDeleteDTO>.Failure("Specialization has link to dieticianSpecializations. Cannot delete specialization");
                    }

                    specialization.isActive = false;

                    _mapper.Map(request.SpecializationDeleteDTO, specialization);

                    _context.SpecializationsDb.Update(specialization);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<SpecializationDeleteDTO>.Failure("Operacja nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<SpecializationDeleteDTO>.Failure("Wystąpił błąd podczas usuwania test results.");
                    }

                    return Result<SpecializationDeleteDTO>.Success(_mapper.Map<SpecializationDeleteDTO>(specialization));
                }
            }
        }
    }
}