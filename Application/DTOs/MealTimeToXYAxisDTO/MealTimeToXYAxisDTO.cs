using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToXYAxisDTO
    {
        // pole Name ale z encji Dish
        //public string Name { get; set; }
        public int MealId { get; set; }
        public string MealTime { get; set; } // Przechowuj czas jako string
        public int? DietId { get; set; }
        public int? DishId { get; set; }

        //public TimeSpan GetMealTimeSpan()
        //{
        //    return TimeSpan.Parse(MealTime);
        //}
    }
}
