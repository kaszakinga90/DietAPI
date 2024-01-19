using Application.DTOs.DieticianDTO;

namespace Application.FiltersExtensions.Dieticians
{
    public static class DieticianExtensions
    {
        public static IQueryable<DieticianGetDTO> DieticianSort(this IQueryable<DieticianGetDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.DieticianName);
            query = orderBy switch
            {
                "dieticianNameAsc" => query.OrderBy(d => d.DieticianName),
                "dieticianNameDesc" => query.OrderByDescending(d => d.DieticianName),
                "email" => query.OrderBy(d => d.Email),
                _ => query.OrderBy(d => d.DieticianName)
            };
            return query;
        }

        public static IQueryable<DieticianGetDTO> DieticianSearch(this IQueryable<DieticianGetDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.DieticianName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<DieticianGetDTO> DieticianFilter(this IQueryable<DieticianGetDTO> query, string dieticianName)
        {
            var dieticianNameList = new List<string>();
            if (!string.IsNullOrEmpty(dieticianName))
                dieticianNameList.AddRange(dieticianName.ToLower().Split(",").ToList());

            query = query.Where(m => dieticianNameList.Count == 0 || dieticianNameList.Contains(m.DieticianName.ToLower()));

            return query;
        }
    }
}