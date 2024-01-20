namespace Application.DTOs.DieticianBusinessCardDTO
{
    public class BusinessCardFiltersDTO
    {
        public List<DateTime> DatesAdded { get; set; }
        public List<string> DieticianNames { get; set; }
        public List<string> SpecializationNames { get; set; }
        public List<string> StateNames { get; set; }
    }
}
