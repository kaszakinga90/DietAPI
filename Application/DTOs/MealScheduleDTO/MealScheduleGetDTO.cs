namespace Application.DTOs.MealScheduleDTO
{
    public class MealScheduleGetDTO
    {
        public int Id { get; set; }
        public int DietId { get; set; }
        public int DishId { get; set; }
        public DateTime MealTime { get; set; } // Data i godzina posiłku
        public string DishName { get; set; }
    }
}
