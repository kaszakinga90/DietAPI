using Application.Core;

namespace Application.FiltersExtensions.Ingredients
{
    public class IngredientsParams:PagingParams
    {
        public string SearchTerm { get; set; }
    }
}
