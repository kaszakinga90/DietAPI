using Application.DTOs.DishDTO;

namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToDietDetailsGetDTO
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        //public Meal Meal { get; set; }
        public DateTime MealTime { get; set; }
        public int? DietId { get; set; }
        public int? DishId { get; set; }
        //public Diet Diet { get; set; }
        public DishGetDTO DishGetDTO { get; set; }
    }
}