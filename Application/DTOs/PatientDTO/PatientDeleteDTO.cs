using Application.DTOs.AddressDTO;

namespace Application.DTOs.PatientDTO
{
    public class PatientDeleteDTO
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public AddressDeleteDTO AddressDeleteDTO { get; set; }
        //public List<NotePatientDeleteDTO> NotesPatientDeleteDTO { get; set; }
    }
}
