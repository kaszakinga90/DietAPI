using Microsoft.AspNetCore.Identity;
using ModelsDB.Functionality;

namespace ModelsDB
{
    public class User: IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isPatient { get; set; }
        public bool isDietician { get; set; }
        public bool isAdmin { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PublicId { get; set; }
        public string PictureUrl { get; set; }
        public Sex Sex { get; set; }
        public int? SexId { get; set; } = null;
        public Address Address { get; set; }
        public int? AddressId { get; set; } = null;
        public bool isDarkMode { get; set; } = false;
        public List<Note> Notes { get; set; }
        public bool isActive { get; set; } = true;
        public DateTime dateAdded { get; set; } = DateTime.Now;
        public DateTime? dateUpdated { get; set; } = null;
        public DateTime? dateDeleted { get; set; } = null;
        public string whoAdded { get; set; }
        public string whoUpdated { get; set; }
        public string whoDeleted { get; set; }
    }
}
