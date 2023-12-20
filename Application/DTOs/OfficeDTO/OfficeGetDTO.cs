using Application.DTOs.AddressDTO;

namespace Application.DTOs.OfficeDTO
{
    public class OfficeGetDTO
    {
        public int Id { get; set; }
        //public int DieticianId { get; set; }
        //public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int AddressId { get; set; }
        public AddressesDTO AddressDTO { get; set; }
    }
}
