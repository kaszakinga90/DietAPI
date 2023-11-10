using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("CategoryOfDiet")]
    public class CategoryOfDiet : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}