using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Recipe")]
    public class Recipe : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Step { get; set; }

        public Dish Dish { get; set; }
        public int DishId { get; set; }
    }
}