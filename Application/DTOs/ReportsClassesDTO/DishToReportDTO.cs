namespace Application.DTOs.ReportsClassesDTO
{
    public class DishToReportDTO
    {
        public string Name { get; set; }
        public string NameEN { get; set; } = null;
        public int? Calories { get; set; }
        public float? ServingQuantity { get; set; }
        //public int? MeasureId { get; set; }
        //public Measure Measure { get; set; }
        public string MeasureName { get; set; }
        public float? Weight { get; set; }
        //public int? UnitId { get; set; }
        //public Unit Unit { get; set; }
        public string UnitName { get; set; }
        public int? GlycemicIndex { get; set; }
        public string PublicId { get; set; }
        public string DishPhotoUrl { get; set; }
        public string PreparingTime { get; set; }
        //public int? RecipeId { get; set; }
        public RecipeToReportDTO Recipe { get; set; }
        public List<DishIngredientToReportDTO> Ingredients { get; set; }
    }
}
