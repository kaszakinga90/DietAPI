using System.Linq;
using Application.DTOs;

namespace Application.FiltersExtensions.DieticianMessages
{
    // Klasa rozszerzeń do zapytań IQueryable dla wiadomości dietetyka.
    public static class DieticianMessagesExtensions
    {
        /// <summary>
        /// Sortuje wiadomości dietetyka według określonego kryterium.
        /// </summary>
        /// <param name="query">Zapytanie do sortowania.</param>
        /// <param name="orderBy">Kryterium sortowania (np. 'name', 'dateAdded').</param>
        /// <returns>Posortowane IQueryable<MessageToDTO>.</returns>
        public static IQueryable<MessageToDTO> PatientSort(this IQueryable<MessageToDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.PatientName);
            query = orderBy switch
            {
                "name" => query.OrderBy(d => d.PatientName),
                "dateAdded" => query.OrderBy(d => d.dateAdded),
                "dateAddedDesc" => query.OrderByDescending(d => d.dateAdded),
                _ => query.OrderBy(d => d.PatientName)
            };
            return query;
        }

        /// <summary>
        /// Filtruje wiadomości dietetyka według terminu wyszukiwania.
        /// </summary>
        /// <param name="query">Zapytanie do filtrowania.</param>
        /// <param name="searchTerm">Termin do wyszukiwania.</param>
        /// <returns>Filtrowane IQueryable<MessageToDTO>.</returns>
        public static IQueryable<MessageToDTO> PatientSearch(this IQueryable<MessageToDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.Description.ToLower().Contains(lowerCaseSearchTerm));
        }

        /// <summary>
        /// Filtruje wiadomości dietetyka według nazwy pacjenta.
        /// </summary>
        /// <param name="query">Zapytanie do filtrowania.</param>
        /// <param name="patientName">Nazwa pacjenta.</param>
        /// <returns>Filtrowane IQueryable<MessageToDTO>.</returns>
        public static IQueryable<MessageToDTO> PatientFilter(this IQueryable<MessageToDTO> query, string patientName)
        {
            var patientNameList = new List<string>();
            if (!string.IsNullOrEmpty(patientName))
                patientNameList.AddRange(patientName.ToLower().Split(",").ToList());

            query = query.Where(m => patientNameList.Count == 0 || patientNameList.Contains(m.PatientName.ToLower()));

            return query;
        }
    }
}