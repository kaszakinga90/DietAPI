namespace Application.CQRS.Ingredients
{
    public class IngredientEditDTO
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string NameEN { get; set; }
        public float Calories { get; set; }
        public float? ServingQuantity { get; set; }
        public int MeasureId { get; set; }
        public float? Weight { get; set; }
        public int UnitId { get; set; }
        public int? DieticianId { get; set; }
        public int? GlycemicIndex { get; set; }
        public List<IngredientNutrientDTO> Nutrients { get; set; }
    }
}