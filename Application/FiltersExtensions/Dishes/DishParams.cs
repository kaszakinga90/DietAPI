using Application.Core;

namespace Application.FiltersExtensions.Dishes
{
    public class DishParams : PagingParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string DishNames { get; set; }
    }
}
