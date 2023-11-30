using Microsoft.AspNetCore.Http;

namespace Application.DTOs.IngredientDTO
{
    public class IngredientGetDTO
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public float Calories { get; set; }
        public int GlycemicIndex { get; set; }
        public float Quantity { get; set; }
        public int MeasureId { get; set; }
        //public IFormFile File { get; set; }
        public string PictureUrl { get; set; }
    }
}
