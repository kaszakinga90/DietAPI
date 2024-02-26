using Application.Core;
using Application.DTOs.AddressDTO;
using Application.DTOs.DieticianBusinessCardDTO;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.OfficeDTO;
using Application.DTOs.SpecializationDTO;
using Application.FiltersExtensions.DieticianBussinesCards;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.DieticiansBusinessesCards
{
    public class DieticianBusinessCardList
    {
        public class Query : IRequest<Result<PagedList<DieticianBusinessCardGetDTO>>>
        {
            public DieticianBussinesCardsParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<DieticianBusinessCardGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<DieticianBusinessCardGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var businessCardsList = _context.DieticiansDb
                    .Where(d => d.isActive)
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
                    .AsQueryable();

                    businessCardsList = businessCardsList.BusinessCardSort(request.Params.OrderBy);
                    businessCardsList = businessCardsList.BusinessCardFilter(request.Params.SpecializationNames, request.Params.StateNames);
                    businessCardsList = businessCardsList.BusinessCardSearch(request.Params.SearchTerm);

                    return Result<PagedList<DieticianBusinessCardGetDTO>>.Success(
                        await PagedList<DieticianBusinessCardGetDTO>.CreateAsync(businessCardsList, request.Params.PageNumber, request.Params.PageSize));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<DieticianBusinessCardGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}