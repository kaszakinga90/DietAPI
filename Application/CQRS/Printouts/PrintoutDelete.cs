using Application.Core;
using Application.DTOs.PrintoutsDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Printouts
{
    public class PrintoutDelete
    {
        public class Command : IRequest<Result<PrintoutDeleteDTO>>
        {
            public int PrintoutId { get; set; }
            public PrintoutDeleteDTO PrintoutDeleteDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<PrintoutDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<PrintoutDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var printout = await _context.PrintoutsDb
                        .SingleOrDefaultAsync(di => di.Id == request.PrintoutId, cancellationToken);

                    if (printout == null)
                    {
                        return Result<PrintoutDeleteDTO>.Failure("Printout template not found.");
                    }

                    printout.isActive = false;

                    _mapper.Map(request.PrintoutDeleteDTO, printout);

                    _context.PrintoutsDb.Update(printout);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<PrintoutDeleteDTO>.Failure("Operacja nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<PrintoutDeleteDTO>.Failure("Wystąpił błąd podczas usuwania szablonu.");
                    }

                    return Result<PrintoutDeleteDTO>.Success(_mapper.Map<PrintoutDeleteDTO>(printout));
                }
            }
        }
    }
}