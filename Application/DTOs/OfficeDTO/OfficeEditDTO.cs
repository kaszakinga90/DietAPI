using Application.DTOs.AddressDTO;

namespace Application.DTOs.OfficeDTO
{
    public class OfficeEditDTO
    {
        public int Id { get; set; }
        public string OfficeName { get; set; }
        public int AddressId { get; set; }
        public AddressesDTO AddressDTO { get; set; }
    }
}
