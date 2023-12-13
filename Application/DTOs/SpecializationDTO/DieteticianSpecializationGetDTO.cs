using ModelsDB.Functionality;

namespace Application.DTOs.SpecializationDTO
{
    public class DieteticianSpecializationGetDTO
    {
        public string DieticianName { get; set; }
        public string SpecializationName { get; set; }
        public List<DieticianSpecialization> DieticianSpecializations { get; set; }
    }
}
