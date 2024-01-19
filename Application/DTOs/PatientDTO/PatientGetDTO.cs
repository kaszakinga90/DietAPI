using Application.DTOs.AddressDTO;
using Microsoft.AspNetCore.Http;

public class PatientGetDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PatientName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public bool isPatient { get; set; }
    public bool isDietician { get; set; }
    public bool isAdmin { get; set; }
    public DateTime? BirthDate { get; set; }
    public IFormFile File { get; set; }
    public string PictureUrl { get; set; }
    public AddressesDTO Address { get; set; }
    public int AddressId { get; set; }
    public List<MessageToDTO> Messages { get; set; }
}