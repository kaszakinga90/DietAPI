using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    [Table("SingleDiet")]
    public class SingleDiet : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int MealTimeHour { get; set; }
        public int MealTimeMinute { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public List<Diet> Diets { get; set; }
        public int DayWeekId { get; set; }
        public DayWeek DayWeek { get; set; }
        public List<MealTime> MealTimes { get; set; }
        public List<CategoryOfDiet> DietCategories { get; set; }

    }
}
