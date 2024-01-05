using Application.DTOs.DishDTO;

namespace Application.FiltersExtensions.Dishes
{
    public static class DishExtensions
    {
        public static IQueryable<DishGetDTO> DishSort(this IQueryable<DishGetDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.Name);
            query = orderBy switch
            {
                "nameAsc" => query.OrderBy(d => d.Name),
                "nameDesc" => query.OrderByDescending(d => d.Name),
                _ => query.OrderBy(d => d.Name)
            };
            return query;
        }

        public static IQueryable<DishGetDTO> DishSearch(this IQueryable<DishGetDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<DishGetDTO> DishFilter(this IQueryable<DishGetDTO> query, string dishName)
        {
            var dishNameList = new List<string>();
            if (!string.IsNullOrEmpty(dishName))
                dishNameList.AddRange(dishName.ToLower().Split(",").ToList());

            query = query.Where(m => dishNameList.Count == 0 || dishNameList.Contains(m.Name.ToLower()));

            return query;
        }
    }
}
