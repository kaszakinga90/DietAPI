namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToXYAxisEditDTO
    {
        public int Id { get; set; }
        public int DietId { get; set; }
        public int DishId { get; set; }
        public DateTime MealTime { get; set; } // Data i godzina posiłku
    }
}
