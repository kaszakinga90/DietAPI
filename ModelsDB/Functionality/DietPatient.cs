using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    public class DietPatient
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DietId { get; set; }
        public Diet Diet { get; set; }
    }
}
