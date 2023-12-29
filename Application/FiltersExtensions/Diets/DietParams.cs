using Application.Core;

namespace Application.FiltersExtensions.Diets
{
    public class DietParams : PagingParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string PatientNames { get; set; }
        public string DieticianNames { get; set; }
    }
}
