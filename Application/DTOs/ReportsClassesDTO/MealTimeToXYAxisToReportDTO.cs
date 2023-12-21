namespace Application.DTOs.ReportsClassesDTO
{
    public class MealTimeToXYAxisToReportDTO
    {
        //public int Id { get; set; }
        //public int? DietId { get; set; }
        //public int? DishId { get; set; }
        public string DishName { get; set; }
        public string MealTime { get; set; }
        public string MealName { get; set; }

        //public Diet Diet { get; set; }
        public DishToReportDTO Dish { get; set; }
        //public int TotalCalories { get; set; }
    }
}