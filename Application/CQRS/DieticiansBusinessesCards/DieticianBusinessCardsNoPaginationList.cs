using Application.Core;
using Application.CQRS.Specializations;
using Application.DTOs.AddressDTO;
using Application.DTOs.DieticianBusinessCardDTO;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.OfficeDTO;
using Application.DTOs.SpecializationDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.DieticiansBusinessesCards
{
    public class DieticianBusinessCardsNoPaginationList
    {
        public class Query : IRequest<Result<List<DieticianBusinessCardGetDTO>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<DieticianBusinessCardGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DieticianBusinessCardGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var businessCardsList = await _context.DieticiansDb
                        .Include(d => d.DieticianOffices)
                            .ThenInclude(o => o.Office)
                                .ThenInclude(a => a.Address)
                        .Include(d => d.Diplomas)
                        .Include(d => d.DieticianSpecializations)
                                .ThenInclude(s => s.Specialization)
                        .Include(d => d.Logo)
                        .Select(d => new DieticianBusinessCardGetDTO
                        {
                            Id = d.Id,
                            DieticianName = d.FirstName + " " + d.LastName,
                            DieticianPictureUrl = d.PictureUrl,
                            DieticianLogoUrl = d.Logo.PictureUrl,
                            DieticianOffices = d.DieticianOffices.Select(o => new OfficeGetDTO
                            {
                                Id = o.Office.Id,
                                OfficeName = o.Office.OfficeName,
                                AddressDTO = new AddressesDTO
                                {
                                    Id = o.Office.Address.Id,
                                    City = o.Office.Address.City,
                                    ZipCode = o.Office.Address.ZipCode,
                                    Country = o.Office.Address.Country,
                                    Street = o.Office.Address.Street,
                                    LocalNo = o.Office.Address.LocalNo,
                                    CountryStateId = o.Office.Address.CountryStateId,
                                    StateName = o.Office.Address.CountryState.StateName
                                }
                            }).ToList(),
                            DieticianDiplomas = d.Diplomas.Select(di => new DiplomaGetDTO
                            {
                                Id = di.Id,
                                Title = di.Title,
                                Description = di.Description,
                                PictureUrl = di.PictureUrl,
                            }).ToList(),
                            DieticianSpecializations = d.DieticianSpecializations.Select(ds => new SpecializationGetDTO
                            {
                                Id = ds.Specialization.Id,
                                SpecializationName = ds.Specialization.SpecializationName,
                            }).ToList(),
                        })
.ToListAsync(cancellationToken);
                    return Result<List<DieticianBusinessCardGetDTO>>.Success(businessCardsList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DieticianBusinessCardGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}
