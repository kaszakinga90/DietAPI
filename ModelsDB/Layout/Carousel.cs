using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Layout
{
    [Table("Carousel")]
    public class Carousel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public LayoutCategory LayoutCategory { get; set; }
        public int LayoutCategoryId { get; set; }
        public List<LayoutPhoto> Photos { get; set; }
    }
}
