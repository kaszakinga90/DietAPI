using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Ingredient")]
    public class Ingredient : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Micronutrient { get; set; }
        public float Macronutrient { get; set; }
        public float Calories { get; set; }
        public float Glycemic { get; set; }
        public float Quantity { get; set; }

        public List<DishIngredient> DishIngredients { get; set; }
    }
}