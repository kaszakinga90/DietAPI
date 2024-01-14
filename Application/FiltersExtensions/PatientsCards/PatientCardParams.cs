using Application.Core;

namespace Application.FiltersExtensions.PatientsCards
{
    public class PatientCardParams:PagingParams
    {
        public string SearchTerm { get; set; }
    }
}
