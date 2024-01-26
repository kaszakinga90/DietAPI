using Application.Core;

namespace Application.FiltersExtensions.DishesFoodCatalog
{
    public class DishesFoodCatalogParams:PagingParams
    {
        public string SearchTerm { get; set; }
    }
}
