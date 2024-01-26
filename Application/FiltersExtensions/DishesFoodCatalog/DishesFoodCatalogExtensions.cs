using Application.DTOs.DishFoodCatalogDTO;

namespace Application.FiltersExtensions.DishesFoodCatalog
{
    public static class DishesFoodCatalogExtensions
    {

        public static IQueryable<DishFoodCatalogGetDTO> DishFoodCatalogSearch(this IQueryable<DishFoodCatalogGetDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.DishName.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
