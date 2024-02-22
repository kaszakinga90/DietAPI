using Application.DTOs.AddressDTO;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.AdminDTO
{
    public class AdminGetDTO
    {
        public int Id { get; set; }
        public string AdminName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public IFormFile File { get; set; }
        public string PictureUrl { get; set; }
        public AddressesDTO AddressDTO { get; set; }
        public List<MessageToDTO> MessagesDTO { get; set; }
    }
}
