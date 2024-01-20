using Application.Core;
using Application.DTOs.AddressDTO;
using Application.DTOs.DieticianDTO;
using Application.FiltersExtensions.Dieticians;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dieticians
{
    public class DieticianList
    {
        public class Query : IRequest<Result<PagedList<DieticianGetDTO>>> 
        {
            public DieticianParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<DieticianGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<DieticianGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dieticiansList = _context.DieticiansDb
                    .Include(a => a.Address)
                    //.ThenInclude(a => a.CountryState)
                    .Select(a => new DieticianGetDTO
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        DieticianName = a.FirstName + " " + a.LastName,
                        Email = a.Email,
                        PhoneNumber = a.PhoneNumber,
                        BirthDate = a.BirthDate,
                        Address = new AddressesDTO
                        {
                            //UserId = a.Address.UserId,
                            City = a.Address.City,
                            //ZipCode = a.Address.ZipCode,
                            //Country = a.Address.Country,
                            //Street = a.Address.Street,
                            //LocalNo = a.Address.LocalNo,
                            //StateName = a.Address.CountryState.StateName
                        }
                    })
                    .AsQueryable();

                    dieticiansList = dieticiansList.DieticianSearch(request.Params.SearchTerm);
                    dieticiansList = dieticiansList.DieticianSort(request.Params.OrderBy);
                    dieticiansList = dieticiansList.DieticianFilter(request.Params.DieticianNames);

                    return Result<PagedList<DieticianGetDTO>>.Success(
                        await PagedList<DieticianGetDTO>.CreateAsync(dieticiansList, request.Params.PageNumber, request.Params.PageSize)
                        );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<DieticianGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}