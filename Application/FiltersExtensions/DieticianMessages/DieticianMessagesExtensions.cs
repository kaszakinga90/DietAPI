namespace Application.FiltersExtensions.DieticianMessages
{
    /// <summary>
    /// Klasa z metodami do sortowania, wyszukiwania i filtrowania dla wiadomości dietetyków
    /// </summary>
    public static class DieticianMessagesExtensions
    {
        /// <summary>
        /// Sortowanie wiadomości po różnych kryteriach
        /// </summary>
        /// <param name="query">Budowane zapytania</param>
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
        /// Wyszukiwanie pacjenta w opisie
        /// </summary>
        /// <param name="query">Budowane zapytanie</param>
        /// <param name="searchTerm">wyszukiwane hasło</param>
        /// <returns>Zbudowane IQueryable<MessageToDTO>.</returns>
        public static IQueryable<MessageToDTO> PatientSearch(this IQueryable<MessageToDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.Description.ToLower().Contains(lowerCaseSearchTerm));
        }

        /// <summary>
        /// Filtrowanie wiadomości według nazw pacjentów
        /// </summary>
        /// <param name="query">Budowane zapytanie</param>
        /// <param name="patientName">Nazwa pacjenta</param>
        /// <returns>Zbudowane IQueryable<MessageToDTO>.</returns>
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