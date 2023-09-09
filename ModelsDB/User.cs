using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("User")]
    public class User : BaseModel
    {
        //komentarz
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


        public Address Address { get; set; }
        public int AddressId { get; set; }
        public List<Note> Notes { get; set; }
    }
}
