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

        //odpowiednik "serving_weight_grams"  odp. np 28
        public float? Weight { get; set; }

        // która to jest jednostka, np. gramy
        public int? UnitId { get; set; }


        public int? GlycemicIndex { get; set; }
        //public IFormFile File { get; set; }
        public string PictureUrl { get; set; }
        public List<IngredientNutrientDTO> NutrientsDTO { get; set; }
    }
}
