using Application.Core;
using Application.DTOs.AdminDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Admins
{
    public class AdminDelete
    {
        public class Command : IRequest<Result<AdminDeleteDTO>>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<AdminDeleteDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<AdminDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var admin = await _context.AdminsDb
                    .Include(d => d.Address)
                    .FirstOrDefaultAsync(i => i.Id == request.Id);

                if (admin == null)
                {
                    return Result<AdminDeleteDTO>.Failure("Admin o podanym ID nie został znaleziony.");
                }

                var adminDTO = _mapper.Map<AdminDeleteDTO>(admin);

                if(adminDTO.isActive == false)
                {
                    return Result<AdminDeleteDTO>.Failure("Admin ma już status USUNIĘTY");
                } 
                else
                {
                    adminDTO.isActive = false;

                    if (adminDTO.AddressDeleteDTO != null)
                    {
                        adminDTO.AddressDeleteDTO.isActive = false;
                    }

                    _mapper.Map(adminDTO, admin);

                    try
                    {
                        var result = await _context.SaveChangesAsync() > 0;
                        if (!result)
                        {
                            return Result<AdminDeleteDTO>.Failure("Usunięcie admina nie powiodło się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Błąd podczas usuwania admina: " + ex);
                        return Result<AdminDeleteDTO>.Failure("Wystąpił błąd podczas usuwania admina.");
                    }

                    return Result<AdminDeleteDTO>.Success(_mapper.Map<AdminDeleteDTO>(admin));
                }
            }
        }
    }
}
