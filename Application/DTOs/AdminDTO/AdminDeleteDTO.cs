using Application.DTOs.AddressDTO;

namespace Application.DTOs.AdminDTO
{
    public class AdminDeleteDTO
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public AddressDeleteDTO AddressDeleteDTO { get; set; }
    }
}
