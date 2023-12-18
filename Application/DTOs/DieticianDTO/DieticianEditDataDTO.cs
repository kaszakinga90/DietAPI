using Microsoft.AspNetCore.Http;
using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.DieticianDTO
{
    public class DieticianEditDataDTO
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
        public string PictureUrl { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
    }
}
