namespace Application.DTOs.IngredientDTO.IngredientNutritionixDTO
{
    public class IngredientNutritionixDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public float Calories { get; set; }
        //odpowiednik "serving_qty"  odp. np 1
        public float? ServingQuantity { get; set; }
        //odpowiednik "serving_unit"  odp. np. "slice (1 oz)"
        public int? MeasureId { get; set; }
        public string MeasureNameFromJSON { get; set; }
        public float? Weight { get; set; }
        public int? UnitId { get; set; }
        public int? GlycemicIndex { get; set; }
        //public string PictureUrl { get; set; }
        public List<IngredientNutrientDTO> NutrientsDTO { get; set; }
    }
}