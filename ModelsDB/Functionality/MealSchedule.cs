using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    public class MealSchedule : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int DietId { get; set; }
        public int MealId { get; set; }
        public DateTime MealTime { get; set; } // Data i godzina posiłku

        public Diet Diet { get; set; }
        public Dish Dish { get; set; }
    }
}
