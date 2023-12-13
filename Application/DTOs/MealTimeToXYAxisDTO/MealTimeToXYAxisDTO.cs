namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToXYAxisDTO
    {
        // pole Name ale z encji Dish
        //public string Name { get; set; }
        public int MealId { get; set; }
        public string MealTime { get; set; }
        public int? DietId { get; set; }
        public int? DishId { get; set; }
    }
}
