using ModelsDB;

namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToXYAxisDTO
    {
        public string Name { get; set; }
        public string MealTime { get; set; } // Przechowuj czas jako string
        public int? DietId { get; set; }

        public TimeSpan GetMealTimeSpan()
        {
            return TimeSpan.Parse(MealTime);
        }
    }
}
