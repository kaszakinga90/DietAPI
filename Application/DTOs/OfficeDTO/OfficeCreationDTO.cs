using Application.DTOs.AddressDTO;
using Application.DTOs.OfficeDTO;

namespace Application.DTOs
{
    public class OfficeCreationDTO
    {
        public OfficePostDTO OfficeDto { get; set; }
        public AddressPostDTO AddressDto { get; set; }
        public int DieticianId { get; set; }
    }
}
