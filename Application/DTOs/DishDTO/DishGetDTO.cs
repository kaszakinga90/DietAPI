using Microsoft.AspNetCore.Http;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.DTOs.DishDTO
{
    public class DishGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Calories { get; set; }   // TODO: zliczane?
        //odpowiednik "serving_qty"  odp. np 1
        public float? ServingQuantity { get; set; }
        //odpowiednik "serving_unit"  odp. np. "slice (1 oz)"
        public int MeasureId { get; set; }
        //odpowiednik "serving_weight_grams"  odp. np 28
        public float? Weight { get; set; }
        // która to jest jednostka, np. gramy
        public int UnitId { get; set; }
        public int? GlycemicIndex { get; set; }  // TODO: jak obliczany?
        public IFormFile File { get; set; }
        public string DishPhotoUrl { get; set; }
        public string PreparingTime { get; set; }   // TODO: jaki format?
        public int RecipeId { get; set; }
        public int? DieteticianId { get; set; }

        // TODO : poniższe powinno działać na listach DTOs
        public List<DishIngredient> DishIngredients { get; set; }
    }
}
