using Application.Core;
using Application.DTOs.AddressDTO;
using Application.DTOs.IngredientDTO;
using Application.DTOs.OfficeDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Offices
{
    public class OfficeDetails
    {
        public class Query : IRequest<Result<OfficeGetDTO>>
        {
            public int OfficeId { get; set; }

            public class Handler : IRequestHandler<Query, Result<OfficeGetDTO>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<OfficeGetDTO>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var office = await _context.OfficesDb
                        .Include(m=>m.Address)
                        .Where(m => m.Id == request.OfficeId)
                        .Select(m => new OfficeGetDTO
                        {
                            Id = m.Id,
                            OfficeName = m.OfficeName,
                            AddressId = m.AddressId,
                            AddressDTO = new AddressesDTO
                            {
                                Id = m.Address.Id,
                                City = m.Address.City,
                                CountryStateId = m.Address.CountryStateId,
                                StateName=m.Address.CountryState.StateName,
                                ZipCode = m.Address.ZipCode,
                                Country = m.Address.Country,
                                Street = m.Address.Street,
                                LocalNo = m.Address.LocalNo
                            }
                        })
                        .FirstOrDefaultAsync();

                    if (office == null)
                    {
                        return Result<OfficeGetDTO>.Failure("Office not found.");
                    }

                    return Result<OfficeGetDTO>.Success(office);
                }
            }
        }
    }
}