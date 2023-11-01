﻿using Microsoft.AspNetCore.Http;
using ModelsDB;

public class PatientDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public bool isPatient { get; set; }
    public bool isDietician { get; set; }
    public bool isAdmin { get; set; }
    public DateTime? BirthDate { get; set; }
    public IFormFile File { get; set; }

    public Address Address { get; set; }
    public int AddressId { get; set; }

    public List<MessageToPatientDTO> Messages { get; set; }
}
