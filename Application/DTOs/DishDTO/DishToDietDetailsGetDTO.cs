using Application.DTOs.DishIngredientDTO;
using Application.DTOs.RecipeDTO;

namespace Application.DTOs.DishDTO
{
    public class DishToDietDetailsGetDTO
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public int? Calories { get; set; }
        public float? ServingQuantity { get; set; }
        public string MeasureName { get; set; }
        public string UnitName { get; set; }
        public int? GlycemicIndex { get; set; }
        public string PreparingTime { get; set; }
        public List<DishIngredientToDietDetailsGetDTO> DishIngredients { get; set; }
        public List<RecipeStepToDietDetailsGetDTO> RecipeStepsDTO { get; set; }
    }
}