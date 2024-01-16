using Application.Core;
using Application.DTOs.AddressDTO;
using Application.FiltersExtensions.Patients;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Patients
{
    public class PatientList
    {
        public class Query : IRequest<Result<PagedList<PatientGetDTO>>> 
        { 
            public PatientParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<PatientGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<PatientGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var patientsList = _context.PatientsDb
                    .Include(a => a.Address)
                    //.ThenInclude(a => a.CountryState)
                    .Select(a => new PatientGetDTO
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        PatientName = a.FirstName + " " + a.LastName,
                        Email = a.Email,
                        PhoneNumber = a.PhoneNumber,
                        BirthDate = a.BirthDate,
                        Address = new AddressesDTO
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

                    patientsList = patientsList.PatientSearch(request.Params.SearchTerm);
                    patientsList = patientsList.PatientSort(request.Params.OrderBy);
                    patientsList = patientsList.PatientFilter(request.Params.PatientNames);

                    return Result<PagedList<PatientGetDTO>>.Success(
                        await PagedList<PatientGetDTO>.CreateAsync(patientsList, request.Params.PageNumber, request.Params.PageSize)
                        );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<PatientGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}