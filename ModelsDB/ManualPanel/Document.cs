using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.ManualPanel
{
    [Table("Document")]
    public class Document : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Content Content { get; set; }
        public int ContentId { get; set; }
        public FileCategory FileCategory { get; set; }
        public int FileCategoryId { get; set; }
    }
}
