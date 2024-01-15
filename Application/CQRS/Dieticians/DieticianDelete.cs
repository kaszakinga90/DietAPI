using Application.Core;
using Application.DTOs.DieticianDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dieticians
{
    public class DieticianDelete
    {
        public class Command : IRequest<Result<DieticianDeleteDTO>>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<DieticianDeleteDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DieticianDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var dietician = await _context.DieticiansDb
                    .Include(d => d.Address)
                    .Include(d => d.Logo)
                    .Include(d => d.DieticianSpecializations)
                        .ThenInclude(d => d.Specialization)
                    .Include(d => d.DieticianOffices)
                        .ThenInclude(o => o.Office)
                            .ThenInclude(o => o.Address)
                    .FirstOrDefaultAsync(i => i.Id == request.Id);

                if (dietician == null)
                {
                    return Result<DieticianDeleteDTO>.Failure("Dietetyk o podanym ID nie został znaleziony.");
                }

                var dieticianDTO = _mapper.Map<DieticianDeleteDTO>(dietician);

                if (dieticianDTO.isActive == false)
                {
                    return Result<DieticianDeleteDTO>.Failure("Dietetyk ma już status USUNIĘTY");
                }
                else
                {
                    dieticianDTO.isActive = false;

                    if (dieticianDTO.AddressDeleteDTO != null)
                    {
                        dieticianDTO.AddressDeleteDTO.isActive = false;
                    }

                    if (dieticianDTO.LogoDeleteDTO != null)
                    {
                        dieticianDTO.LogoDeleteDTO.isActive = false;
                    }

                    foreach (var ds in dieticianDTO.DieticianSpecializationDeleteDTO)
                    {
                        if (ds.DieticianId == request.Id)
                        {
                            ds.isActive = false;
                            _mapper.Map(dieticianDTO.DieticianSpecializationDeleteDTO, dietician.DieticianSpecializations);
                        }
                    }

                    // TODO : to powinno działać w całości na obiektach DTO
                    // to jest DOBRE !!!!
                    foreach (var officeDTO in dieticianDTO.DieticianOfficesDeleteDTO)
                    {
                        var dieticianOffice = await _context.DieticianOffices
                            .Include(o => o.Office)
                                .ThenInclude(o => o.Address)
                            .FirstOrDefaultAsync(o => o.DieticianId == officeDTO.DieticianId && o.OfficeId == officeDTO.OfficeDeleteDTO.Id);

                        if (dieticianOffice != null)
                        {
                            dieticianOffice.isActive = false;

                            if (dieticianOffice.Office != null)
                            {
                                dieticianOffice.Office.isActive = false;

                                if (dieticianOffice.Office.Address != null)
                                {
                                    dieticianOffice.Office.Address.isActive = false;
                                }

                                _context.OfficesDb.Update(dieticianOffice.Office);
                            }

                            _context.DieticianOffices.Update(dieticianOffice);
                        }
                    }


                    _mapper.Map(dieticianDTO, dietician);
                    
                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<DieticianDeleteDTO>.Failure("Usunięcie dietetyka nie powiodło się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Błąd podczas usuwania dietetyka: " + ex);
                        return Result<DieticianDeleteDTO>.Failure("Wystąpił błąd podczas usuwania dietetyka.");
                    }

                    return Result<DieticianDeleteDTO>.Success(_mapper.Map<DieticianDeleteDTO>(dietician));
                }
            }
        }
    }
}
