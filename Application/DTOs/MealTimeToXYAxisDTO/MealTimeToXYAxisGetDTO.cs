using ModelsDB;

namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToXYAxisGetDTO
    {
        public string Name { get; set; }
        public DateTime MealTime { get; set; }
        public int? DietId { get; set; }
        public Diet Diet { get; set; }
    }
}
