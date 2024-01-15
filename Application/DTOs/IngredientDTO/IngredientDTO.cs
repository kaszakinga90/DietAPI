using Microsoft.AspNetCore.Http;
// TODO : 
namespace Application.DTOs.IngredientDTO
{
    public class IngredientDTO
    {
        public string Id { get; set; }
        public string IngredientName { get; set; }
        public float Calories { get; set; }
        //odpowiednik "serving_qty"  odp. np 1
        public float? ServingQuantity { get; set; }
        //odpowiednik "serving_unit"  odp. np. "slice (1 oz)"
        public int? MeasureId { get; set; }
        public float? Weight { get; set; }
        public int? UnitId { get; set; }
        public int? GlycemicIndex { get; set; }
        public int DieticianId { get; set; }
        //public List<IngredientNutrientDTO> NutrientsDTO { get; set; }
    }
}
