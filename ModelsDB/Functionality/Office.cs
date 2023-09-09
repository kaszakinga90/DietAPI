using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Office")]
    public class Office : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public List<Address> Addresses { get; set; }
        public List<Dietician> Dietician { get; set; }
    }
}