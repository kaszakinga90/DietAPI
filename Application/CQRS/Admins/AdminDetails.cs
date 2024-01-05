using Application.Core;
using Application.DTOs.AddressDTO;
using Application.DTOs.AdminDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Admins
{
    public class AdminDetails
    {
        public class Query : IRequest<Result<AdminGetDTO>>
        {
            public int AdminId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<AdminGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<AdminGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var admin = await _context.AdminsDb
                               .Include(a => a.Address)
                                    .ThenInclude(a => a.CountryState)
                               .Include(a => a.MessageTo)
                               .Select(a => new AdminGetDTO
                               {
                                   Id = a.Id,
                                   AdminName = a.FirstName + " " + a.LastName,
                                   Email = a.Email,
                                   PhoneNumber = a.PhoneNumber,
                                   BirthDate = a.BirthDate,
                                   AddressDTO = new AddressesDTO
                                   {
                                       Id = a.Address.Id,
                                       City = a.Address.City,
                                       ZipCode = a.Address.ZipCode,
                                       Country = a.Address.Country,
                                       Street = a.Address.Street,
                                       LocalNo = a.Address.LocalNo,
                                       CountryStateId = a.Address.CountryStateId,
                                       StateName = a.Address.CountryState.StateName
                                   },
                                   MessagesDTO = a.MessageTo.Select(m => new MessageToDTO
                                   {
                                       Id = m.Id,
                                       Title = m.Title,
                                       Description = m.Description,
                                       AdminName = m.Admin.FirstName + " " + m.Admin.LastName,
                                       DieticianName = m.Dietician.FirstName + " " + m.Dietician.LastName,
                                       PatientName = m.Patient.FirstName + " " + m.Patient.LastName,
                                   }).ToList()
                               })
                               .SingleOrDefaultAsync(x => x.Id == request.AdminId, cancellationToken);

                if (admin == null)
                {
                    return Result<AdminGetDTO>.Failure("Admin nie został znaleziony.");
                }

                return Result<AdminGetDTO>.Success(admin);
            }
        }
    }
}
