using ModelsDB;

namespace Application.DTOs.PatientDTO
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public bool isPatient { get; set; }
        public bool isDietician { get; set; }
        public bool isAdmin { get; set; }
        public DateTime? BirthDate { get; set; }

        public Address Address { get; set; }
        public int AddressId { get; set; }

        public List<MessageToDTO> Messages { get; set; }
    }
}