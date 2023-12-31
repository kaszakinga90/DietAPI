﻿using Application.Core;
using Application.DTOs.AddressDTO;
using Application.DTOs.AdminDTO;
using Application.FiltersExtensions.Admins;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                var adminsList = _context.AdminsDb
                    .Include(a => a.Address)
                        //.ThenInclude(a => a.CountryState)
                    .Select(a => new AdminGetDTO
                    {
                        Id = a.Id,
                        AdminName = a.FirstName + " " + a.LastName,
                        Email = a.Email,
                        Phone = a.PhoneNumber,
                        BirthDate = a.BirthDate,
                        AddressDTO = new AddressesDTO
                        {
                            //Id = a.Address.Id,
                            City = a.Address.City,
                            //ZipCode = a.Address.ZipCode,
                            //Country = a.Address.Country,
                            //Street = a.Address.Street,
                            //LocalNo = a.Address.LocalNo,
                            //StateName = a.Address.CountryState.StateName
                        }
                    })
                    .AsQueryable();

                if (adminsList == null)
                {
                    return Result<PagedList<AdminGetDTO>>.Failure("no results.");
                }

                adminsList = adminsList.AdminSearch(request.Params.SearchTerm);
                adminsList = adminsList.AdminSort(request.Params.OrderBy);
                adminsList = adminsList.AdminFilter(request.Params.AdminNames);

                return Result<PagedList<AdminGetDTO>>.Success(
                    await PagedList<AdminGetDTO>.CreateAsync(adminsList, request.Params.PageNumber, request.Params.PageSize)
                    );
            }
        }
    }
}
