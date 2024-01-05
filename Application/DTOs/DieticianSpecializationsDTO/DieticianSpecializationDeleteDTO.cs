namespace Application.DTOs.DieticianSpecializationsDTO
{
    public class DieticianSpecializationDeleteDTO
    {
        public int DieticianId { get; set; }
        public int SpecializationId { get; set; }
        public bool isActive { get; set; }
    }
}
