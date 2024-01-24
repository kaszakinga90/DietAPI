namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToXYAxisGetDTO
    {
        public int Id { get; set; }
        public int? DietId { get; set; }
        public int? DishId { get; set; }
        public string DishName { get; set; }
        public DateTime MealTime { get; set; }
    }
}