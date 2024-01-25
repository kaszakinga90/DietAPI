using Application.DTOs.DishDTO;

namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToDietDetailsGetDTO
    {
        public string MealDate { get; set; }
        public string MealTime { get; set; }
        public string MealName { get; set; }
        public DishToDietDetailsGetDTO DishToDietDetailsGetDTO { get; set; }
    }
}