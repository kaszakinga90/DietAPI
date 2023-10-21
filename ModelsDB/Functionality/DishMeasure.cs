using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    public class DishMeasure
    {
        public int DishId { get; set; }
        public Dish Dish { get; set; }

        public int MeasureId { get; set; }
        public Measure Measure { get; set; }
    }
}
