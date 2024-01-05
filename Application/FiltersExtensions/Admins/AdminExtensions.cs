using Application.DTOs.AdminDTO;

namespace Application.FiltersExtensions.Admins
{
    public static class AdminExtensions
    {
        public static IQueryable<AdminGetDTO> AdminSort(this IQueryable<AdminGetDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.AdminName);
            query = orderBy switch
            {
                "adminNameAsc" => query.OrderBy(d => d.AdminName),
                "adminNameDesc" => query.OrderByDescending(d => d.AdminName),
                "email" => query.OrderBy(d => d.Email),
                _ => query.OrderBy(d => d.AdminName)
            };
            return query;
        }

        public static IQueryable<AdminGetDTO> AdminSearch(this IQueryable<AdminGetDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.AdminName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<AdminGetDTO> AdminFilter(this IQueryable<AdminGetDTO> query, string adminName)
        {
            var adminNameList = new List<string>();
            if (!string.IsNullOrEmpty(adminName))
                adminNameList.AddRange(adminName.ToLower().Split(",").ToList());

            query = query.Where(m => adminNameList.Count == 0 || adminNameList.Contains(m.AdminName.ToLower()));

            return query;
        }
    }
}
