using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("Term")]
    public class Term : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Visit> Visits { get; set; }
    }
}