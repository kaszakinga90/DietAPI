using Application.Core;

namespace Application.FiltersExtensions.DieticianBussinesCards
{
    public class DieticianBussinesCardsParams : PagingParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string StateNames { get; set; }
        public string SpecializationNames { get; set; }
    }
}
