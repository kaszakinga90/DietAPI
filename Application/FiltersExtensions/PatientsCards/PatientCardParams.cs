using Application.Core;

namespace Application.FiltersExtensions.PatientsCards
{
    public class PatientCardParams:PagingParams
    {
        public string SearchTerm { get; set; }
        public string OrderBy { get; set; }
        public string PatientNames { get; set; }
    }
}