using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    public class DieticianSpecialization
    {
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
