using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("Sex")]
    public class Sex : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
