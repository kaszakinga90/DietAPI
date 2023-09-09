using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Layout
{
    [Table("Article")]
    public class Article : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public LayoutCategory LayoutCategory { get; set; }
        public int LayoutCategoryId { get; set; }
        public List<Tag> Tags { get; set; }
        public List<LayoutPhoto> Photos { get; set; }


        //[Required]
        //[MaxLength(10, ErrorMessage = "BloggerName must be 10 characters or less"), MinLength(5)]
        //[Column("BlogDescription", TypeName = "ntext")]
        //[Display(Name = "Choose category")]
    }
}
