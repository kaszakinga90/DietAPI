﻿namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToXYAxisPostDTO
    {
        public int MealId { get; set; }
        public string MealTime { get; set; }
        public int? DietId { get; set; }
        public int? DishId { get; set; }
    }
}
