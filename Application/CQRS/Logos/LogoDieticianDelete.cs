using Application.Core;
using Application.DTOs.LogoDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Logos
{
    public class LogoDieticianDelete
    {
        public class Command : IRequest<Result<LogoPostDTO>>
        {
            public int DieticianId { get; set; }
            public LogoPostDTO LogoPostDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<LogoPostDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<LogoPostDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var logo = await _context.LogosDb
                        .SingleOrDefaultAsync(l => l.DieticianId == request.DieticianId);

                    if (logo == null)
                    {
                        return Result<LogoPostDTO>.Failure("logo not found.");
                    }

                    _mapper.Map(request.LogoPostDTO, logo);

                    if (logo != null)
                    {
                        logo.isActive = false;

                        try
                        {
                            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                            if (!result)
                            {
                                return Result<LogoPostDTO>.Failure("Operacja nie powiodła się.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                            return Result<LogoPostDTO>.Failure("Wystąpił błąd podczas usuwania logo.");
                        }
                    }
                    return Result<LogoPostDTO>.Success(_mapper.Map<LogoPostDTO>(logo));
                }
            }
        }
    }
}