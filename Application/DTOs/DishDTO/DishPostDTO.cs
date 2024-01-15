using Application.DTOs.MealTimeToXYAxisDTO;
using Application.DTOs.DishFoodCatalogDTO;
using Application.DTOs.DishIngredientDTO;
using Application.DTOs.RecipeStepDTO;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.DishDTO
{
    public class DishPostDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Calories { get; set; }   // TODO: zliczane?
        //odpowiednik "serving_qty"  odp. np 1
        public float? ServingQuantity { get; set; }
        //odpowiednik "serving_unit"  odp. np. "slice (1 oz)"
        public int? MeasureId { get; set; }
        public int UnitId { get; set; }
        public int? GlycemicIndex { get; set; }  // TODO: jak obliczany?
        public string PreparingTime { get; set; }
        public int? RecipeId { get; set; } = null;
        public int DieteticianId { get; set; }
        public List<RecipeStepPostDTO> RecipeSteps { get; set; }
        public List<DishFoodCatalogPostDTO> DishFoodCatalogs { get; set; }
        public List<DishIngredientPostDTO> DishIngredients { get; set; }
        //public List<MealTimeToXYAxisPostDTO> MealTimes { get; set; }
    }
}
