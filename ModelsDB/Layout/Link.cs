using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Layout
{
    [Table("Link")]
    public class Link : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Footer Footer { get; set; }
        public int FooterId { get; set; }
        public SubTab SubTab { get; set; }
        public LayoutCategory LayoutCategory { get; set; }
        public int LayoutCategoryId { get; set; }
        public SocialMedia SocialMedia { get; set; }
        public int SocialMediaId { get; set; }
    }
}
