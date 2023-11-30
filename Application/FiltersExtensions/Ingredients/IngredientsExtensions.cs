using Application.DTOs.IngredientDTO;

namespace Application.FiltersExtensions.Ingredients
{
    public static class IngredientsExtensions
    {
        public static IQueryable<IngredientGetDTO> Search(this IQueryable<IngredientGetDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.IngredientName.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
