namespace Application.DTOs.IngredientDTO
{
    public class IngredientGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Calories { get; set; }
        public int GlycemicIndex { get; set; }
        public float ServingQuantity { get; set; }
        public int MeasureId { get; set; }
        public int? UnitId { get; set; }
        //public IFormFile File { get; set; }
        public string PictureUrl { get; set; }
        public float? Weight { get; set; }
    }
}
