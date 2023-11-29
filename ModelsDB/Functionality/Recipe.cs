using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Recipe")]
    public class Recipe : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public List<RecipeStep> Steps { get; set; }
    }
}