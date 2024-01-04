using Application.DTOs.OfficeDTO;

namespace Application.DTOs.DieticianOfficeDTO
{
    public class DieticianOfficeDeleteDTO
    {
        public int DieticianId { get; set; }
        public OfficeDeleteDTO OfficeDeleteDTO { get; set; }
        public bool isActive { get; set; }
    }
}
