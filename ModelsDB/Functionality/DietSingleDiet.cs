using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    public class DietSingleDiet
    {
        public int DietId { get; set; }
        public Diet Diet { get; set; }

        public int SingleDietId { get; set; }
        public SingleDiet SingleDiet { get; set; }
    }
}
