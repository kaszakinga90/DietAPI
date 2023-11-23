using ModelsDB.Functionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SpecializationDTO
{
    public class DieteticianSpecializationGetDTO
    {
        public string DieticianName { get; set; }
        public string SpecializationName { get; set; }
        public List<DieticianSpecialization> DieticianSpecializations { get; set; }
    }
}
