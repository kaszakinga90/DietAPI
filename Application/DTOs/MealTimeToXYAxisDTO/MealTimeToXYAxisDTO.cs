namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToXYAxisDTO
    {
        // pole Name ale z encji Dish
        //public string Name { get; set; }
        public int MealId { get; set; }
        public DateTime MealTime { get; set; } // Przechowuj czas jako string
        public int? DietId { get; set; }
        public int? DishId { get; set; }
    }
}
