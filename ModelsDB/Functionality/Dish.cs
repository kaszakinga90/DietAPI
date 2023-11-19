using ModelsDB.Functionality;
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
        //public float Micronutrient { get; set; }
        //public float Macronutrient { get; set; }
        //public float Calories { get; set; }
        //public float Glycemic { get; set; }
        //public string DishPhotoUrl { get; set; }

        //public List<DishFoodCatalog> DishFoodCatalogs { get; set; }
        //public List<DishIngredient> DishIngredients { get; set; }
        //public List<DishMeasure> DishMeasures { get; set; }
        //public int RecipesId { get; set; }
        //public Recipe Recipe { get; set; }

        public List<MealSchedule> MealSchedules { get; set; }
    }
}