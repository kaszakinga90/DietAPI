using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("IngredientNutrient")]
    public class IngredientNutrient : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        [ForeignKey("Nutrient")]
        public int NutrientId { get; set; }
        public Nutrient Nutrient { get; set; }

        // wartość nutrient wchodzącego w skład ingredient (czyli np. ile w serze jest witaminy D)
        public float NutrientValue { get; set; }
    }
}
