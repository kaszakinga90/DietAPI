using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Layout
{
    [Table("News")]
    public class News : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<LayoutPhoto> Photos { get; set; }
    }
}
