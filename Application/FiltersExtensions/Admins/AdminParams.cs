using Application.Core;

namespace Application.FiltersExtensions.Admins
{
    public class AdminParams : PagingParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string AdminNames { get; set; }
    }
}