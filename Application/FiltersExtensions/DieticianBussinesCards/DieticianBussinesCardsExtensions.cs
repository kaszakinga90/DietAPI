using Application.DTOs.AddressDTO;
using Application.DTOs.CountryStateDTO;
using Application.DTOs.DieticianBusinessCardDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FiltersExtensions.DieticianBussinesCards
{
    public static class DieticianBussinesCardsExtensions
    {
        //public static IQueryable<MessageToDTO> PatientSort(this IQueryable<MessageToDTO> query, string orderBy)
        //{
        //    if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.PatientName);
        //    query = orderBy switch
        //    {
        //        "dateAdded" => query.OrderBy(d => d.dateAdded),
        //        "dateAddedDesc" => query.OrderByDescending(d => d.dateAdded),
        //        _ => query.OrderBy(d => d.PatientName)
        //    };
        //    return query;
        //}
        //public static IQueryable<DieticianBusinessCardGetDTO> Search(this IQueryable<DieticianBusinessCardGetDTO> query, string searchTerm)
        //{
        //    if (string.IsNullOrWhiteSpace(searchTerm)) return query;
        //    var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
        //    return query.Where(p => p.City.ToLower().Contains(lowerCaseSearchTerm) ||
        //                    p.Country.ToLower().Contains(lowerCaseSearchTerm) ||
        //                    p.Street.ToLower().Contains(lowerCaseSearchTerm));
        //}
        public static IQueryable<CountryStateGetDTO> Filter(this IQueryable<CountryStateGetDTO> query, string stateName)
        {
            var countryStateList = new List<string>();
            if (!string.IsNullOrEmpty(stateName))
                countryStateList.AddRange(stateName.ToLower().Split(",").ToList());

            query = query.Where(m => countryStateList.Count == 0 || countryStateList.Contains(m.StateName.ToLower()));

            return query;
        }
    }
}