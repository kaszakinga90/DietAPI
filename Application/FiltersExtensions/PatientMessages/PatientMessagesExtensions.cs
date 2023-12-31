namespace Application.FiltersExtensions.PatientMessages
{
    public static class PatientMessagesExtensions
    {
        public static IQueryable<MessageToDTO> Sort(this IQueryable<MessageToDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.DieticianName);
            query = orderBy switch
            {
                "name" => query.OrderBy(d => d.DieticianName),
                "dateAdded" => query.OrderBy(d => d.dateAdded),
                "dateAddedDesc" => query.OrderByDescending(d => d.dateAdded),
                _ => query.OrderBy(d => d.DieticianName)
            };
            return query;
        }
        public static IQueryable<MessageToDTO> Search(this IQueryable<MessageToDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.Description.ToLower().Contains(lowerCaseSearchTerm));
        }
        public static IQueryable<MessageToDTO> Filter(this IQueryable<MessageToDTO> query, string dieticianName)
        {
            var dieticianNameList = new List<string>();
            if (!string.IsNullOrEmpty(dieticianName))
                dieticianNameList.AddRange(dieticianName.ToLower().Split(",").ToList());

            query = query.Where(m => dieticianNameList.Count == 0 ||  dieticianNameList.Contains(m.DieticianName.ToLower()));

            return query;
        }
    }
}