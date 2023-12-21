using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MealTimeToXYAxisDTO
{
    public class MealTimeToXYAxisToReportDTO
    {
        public int Id { get; set; }
        public int? DietId { get; set; }
        public int? DishId { get; set; }
        public string DishName { get; set; }
        public DateTime MealTime { get; set; }
    }
}
