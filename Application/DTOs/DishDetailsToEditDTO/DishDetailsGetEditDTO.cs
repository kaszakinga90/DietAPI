using ModelsDB;

namespace Application.DTOs.DishDetailsToEditDTO
{
    public class DishDetailsGetEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Calories { get; set; }
        public float? ServingQuantity { get; set; }
        public int? MeasureId { get; set; }
        public int? UnitId { get; set; }
        public int? GlycemicIndex { get; set; }
        public string PreparingTime { get; set; }
    }
}