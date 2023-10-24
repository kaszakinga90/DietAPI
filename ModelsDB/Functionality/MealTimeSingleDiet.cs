using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    public class MealTimeSingleDiet
    {
        public int MealTimeId { get; set; }
        public MealTime MealTime { get; set; }

        public int SingleDietId { get; set; }
        public SingleDiet SingleDiet { get; set; }
    }
}
