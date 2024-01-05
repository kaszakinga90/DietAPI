using Application.DTOs.AddressDTO;

namespace Application.DTOs.OfficeDTO
{
    public class OfficeDeleteDTO
    {
        public int Id { get; set; }
        public AddressDeleteDTO AddressDeleteDTO { get; set; }
        public bool isActive { get; set; }
    }
}
