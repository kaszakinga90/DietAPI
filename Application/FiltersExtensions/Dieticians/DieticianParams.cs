using Application.Core;

namespace Application.FiltersExtensions.Dieticians
{
    public class DieticianParams : PagingParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string DieticianNames { get; set; }
    }
}