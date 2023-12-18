using Application.Core;

namespace Application.FiltersExtensions.PatientMessages
{
    public class PatientMessagesParams : PagingParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string DieticianNames { get; set; }
    }
}
