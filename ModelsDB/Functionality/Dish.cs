using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace ModelsDB
{
    [Table("Dish")]
    public class Dish : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Micronutrient { get; set; }
        public float Macronutrient { get; set; }
        public float Calories { get; set; }
        public float Glycemic { get; set; }
        public string DishPhotoUrl { get; set; }

        public List<FoodCatalog> FoodCatalogs { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Measure> Measures { get; set; }
        public int RecipesId { get; set; }
        public Recipe Recipe { get; set; }
    }
}