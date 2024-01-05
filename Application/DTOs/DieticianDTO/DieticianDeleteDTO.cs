using Application.DTOs.AddressDTO;
using Application.DTOs.DieticianOfficeDTO;
using Application.DTOs.DieticianSpecializationsDTO;
using Application.DTOs.LogoDTO;

namespace Application.DTOs.DieticianDTO
{
    public class DieticianDeleteDTO
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public AddressDeleteDTO AddressDeleteDTO { get; set; }
        public LogoDeleteDTO LogoDeleteDTO { get; set; }
        public List<DieticianSpecializationDeleteDTO> DieticianSpecializationDeleteDTO { get; set; }
        public List<DieticianOfficeDeleteDTO> DieticianOfficesDeleteDTO { get; set; }
    }
}
