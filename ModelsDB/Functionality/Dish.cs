using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Dish")]
    public class Dish : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; } = null;
        public int? Calories { get; set; }   // TODO: zliczane?
        //odpowiednik "serving_qty"  odp. np 1
        public float? ServingQuantity { get; set; }
        //odpowiednik "serving_unit"  odp. np. "slice (1 oz)"
        public int? MeasureId { get; set; }
        public Measure Measure { get; set; }
        //odpowiednik "serving_weight_grams"  odp. np 28
        public float? Weight { get; set; }
        public int? UnitId { get; set; }
        public Unit Unit { get; set; }
        public int? GlycemicIndex { get; set; }  // TODO: jak obliczany?
        public string PublicId { get; set; }
        public string DishPhotoUrl { get; set; }
        public string PreparingTime { get; set; }   // TODO: jaki format?
        public int? RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int? DieteticianId { get; set; }
        public Dietician Dietician { get; set; }
        public List<DishFoodCatalog> DishFoodCatalogs { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public List<MealTimeToXYAxis> MealTimes { get; set; }
    }
}