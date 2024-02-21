using Application.Core;
using Application.DTOs.OfficeDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Offices
{
    public class OfficeDelete
    {
        public class Command : IRequest<Result<OfficeDeleteDTO>>
        {
            public int DieticianId { get; set; }
            public int OfficeId { get; set; }
            public OfficeDeleteDTO OfficeDeleteDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<OfficeDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<OfficeDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var office = await _context.OfficesDb
                        .Include(o => o.Address)
                        .Include(o => o.DieticianOffices)
                        .SingleOrDefaultAsync(di => di.Id == request.OfficeId && di.DieticianOffices.Any(d => d.DieticianId == request.DieticianId), cancellationToken);

                    if (office == null)
                    {
                        return Result<OfficeDeleteDTO>.Failure("Nie znaleziono biura.");
                    }

                    office.isActive = false;
                    office.Address.isActive = false;

                    foreach (var dieticianOffice in office.DieticianOffices)
                    {
                        dieticianOffice.isActive = false;
                    }

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<OfficeDeleteDTO>.Failure("Operacja nie powiodła się.");
                        }
                        return Result<OfficeDeleteDTO>.Success(_mapper.Map<OfficeDeleteDTO>(office));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<OfficeDeleteDTO>.Failure("Wystąpił błąd podczas usuwania test results.");
                    } 
                }
            }
        }
    }
}