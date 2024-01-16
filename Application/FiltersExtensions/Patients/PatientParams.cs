using Application.Core;

namespace Application.FiltersExtensions.Patients
{
    public class PatientParams : PagingParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string PatientNames { get; set; }
    }
}