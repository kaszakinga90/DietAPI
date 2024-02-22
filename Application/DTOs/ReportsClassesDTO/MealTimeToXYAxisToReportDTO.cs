namespace Application.DTOs.ReportsClassesDTO
{
    public class MealTimeToXYAxisToReportDTO
    {
        public string DishName { get; set; }
        public string MealTime { get; set; }
        public string MealName { get; set; }
        public DishToReportDTO Dish { get; set; }
    }
}