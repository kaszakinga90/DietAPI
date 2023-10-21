using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    public class DieticianOffice
    {
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }

        public int OfficeId { get; set; }
        public Office Office { get; set; }
    }
}
