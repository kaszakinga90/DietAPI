using Application.DTOs.DishFoodCatalogDTO;
using Application.DTOs.DishIngredientDTO;
using Application.DTOs.RecipeStepDTO;

namespace Application.DTOs.DishDTO
{
    public class DishGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Calories { get; set; }
        //odpowiednik "serving_qty"  odp. np 1
        public float? ServingQuantity { get; set; }
        //odpowiednik "serving_unit"  odp. np. "slice (1 oz)"
        public int MeasureId { get; set; }
        //odpowiednik "serving_weight_grams"  odp. np 28
        public int UnitId { get; set; }
        public int? GlycemicIndex { get; set; }
        public string PreparingTime { get; set; }
        public int RecipeId { get; set; }
        public int? DieteticianId { get; set; }
        public List<DishIngredientGetDTO> DishIngredients { get; set; }
        public List<DishFoodCatalogGetDTO> DishFoodCatalogs { get; set; }
        public List<RecipeStepGetDTO> RecipeStepsDTO { get; set; }
        public List<DishFoodCatalogGetDTO> DishFoodCatalogs { get; set; }
    }
}
