using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Office")]
    public class Office : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string OfficeName { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public List<DieticianOffice> DieticianOffices { get; set; }
    }
}