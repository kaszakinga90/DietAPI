using Application.DTOs.DiplomaDTO;
using Application.DTOs.OfficeDTO;
using Application.DTOs.SpecializationDTO;

namespace Application.DTOs.DieticianBusinessCardDTO
{
    public class DieticianBusinessCardGetDTO
    {
        public int Id { get; set; }
        public string DieticianName { get; set; }
        public string DieticianPictureUrl { get; set; }
        public List<OfficeGetDTO> DieticianOffices { get; set; }
        public string DieticianLogoUrl { get; set; }
        public List<DiplomaGetDTO> DieticianDiplomas { get; set; }
        public List<SpecializationGetDTO> DieticianSpecializations { get; set; }
    }
}
