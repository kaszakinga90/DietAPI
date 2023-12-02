using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ModelsDB
{
    public class User: IdentityUser<int>
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool isPatient { get; set; }
        public bool isDietician { get; set; }
        public bool isAdmin { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PublicId { get; set; }
        public string PictureUrl { get; set; }



        public Address Address { get; set; }
        public int? AddressId { get; set; } = null;
        public List<Note> Notes { get; set; }

        public bool isActive { get; set; } = true;
        public DateTime dateAdded { get; set; } = DateTime.Now;
        public DateTime? dateUpdated { get; set; } = null;  // Nullowalna data
        public DateTime? dateDeleted { get; set; } = null;  // Nullowalna data
        public string whoAdded { get; set; }
        public string whoUpdated { get; set; }
        public string whoDeleted { get; set; }
    }
}
