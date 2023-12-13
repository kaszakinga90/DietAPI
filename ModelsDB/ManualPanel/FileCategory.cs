using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.ManualPanel
{
    [Table("FileCategory")]
    public class FileCategory : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public CategoryType CategoryType { get; set; }
        public int CategoryTypeId { get; set; }
        public List<Manual> Manuals { get; set; }
    }
}
