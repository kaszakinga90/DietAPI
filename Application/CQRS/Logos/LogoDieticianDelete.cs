using Application.Core;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.LogoDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using System.Diagnostics;

namespace Application.CQRS.Logos
{
    public class LogoDieticianDelete
    {
        public class Command : IRequest<Result<LogoDeleteDTO>>
        {
            public int DieticianId { get; set; }
            public LogoDeleteDTO LogoDeleteDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<LogoDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<LogoDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var logo = await _context.LogosDb
                        .SingleOrDefaultAsync(l => l.DieticianId == request.DieticianId);

                    if (logo == null)
                    {
                        return Result<LogoDeleteDTO>.Failure("logo not found.");
                    }

                    _mapper.Map(request.LogoDeleteDTO, logo);

                    if (logo != null)
                    {
                        logo.PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1705234573/qc1pclrxojm3arwfkrif.jpg";

                        _context.LogosDb.Update(logo);

                        try
                        {
                            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                            if (!result)
                            {
                                return Result<LogoDeleteDTO>.Failure("Operacja nie powiodła się.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                            return Result<LogoDeleteDTO>.Failure("Wystąpił błąd podczas usuwania logo.");
                        }
                    }
                    return Result<LogoDeleteDTO>.Success(_mapper.Map<LogoDeleteDTO>(logo));
                }
            }
        }
    }
}