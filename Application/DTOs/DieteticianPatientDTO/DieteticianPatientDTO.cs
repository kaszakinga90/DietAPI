using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.DieteticianPatientDTO
{
    public class DieteticianPatientDTO
    {
        public int PatientId { get; set; }

        public int DieticianId { get; set; }
        public string DieteticianName { get; set; }
        public string PatientName { get; set; }
    }
}
