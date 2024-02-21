namespace Application.DTOs.IngredientDTO
{
    public class IngredientPutDTO
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public float Calories { get; set; }
        public int GlycemicIndex { get; set; }
        public float ServingQuantity { get; set; }
        public int MeasureId { get; set; }
        public int? UnitId { get; set; }
        public string PictureUrl { get; set; }
        public float? Weight { get; set; }
    }
}
