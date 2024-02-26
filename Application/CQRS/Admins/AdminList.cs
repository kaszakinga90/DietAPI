using Application.Core;
using Application.DTOs.AddressDTO;
using Application.DTOs.AdminDTO;
using Application.FiltersExtensions.Admins;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Admins
{
    public class AdminList
    {
        public class Query : IRequest<Result<PagedList<AdminGetDTO>>> 
        {
            public AdminParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<AdminGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<AdminGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var adminsList = _context.AdminsDb
                    .Where(m => m.isActive)
                    .Include(a => a.Address)
                    .Select(a => new AdminGetDTO
                    {
                        Id = a.Id,
                        AdminName = a.FirstName + " " + a.LastName,
                        Email = a.Email,
                        PhoneNumber = a.PhoneNumber,
                        BirthDate = a.BirthDate,
                        AddressDTO = new AddressesDTO
                        {
                            City = a.Address.City,
                        }
                    })
                    .AsQueryable();

                    adminsList = adminsList.AdminSearch(request.Params.SearchTerm);
                    adminsList = adminsList.AdminSort(request.Params.OrderBy);
                    adminsList = adminsList.AdminFilter(request.Params.AdminNames);

                    return Result<PagedList<AdminGetDTO>>.Success(
                        await PagedList<AdminGetDTO>.CreateAsync(adminsList, request.Params.PageNumber, request.Params.PageSize)
                        );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<AdminGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}