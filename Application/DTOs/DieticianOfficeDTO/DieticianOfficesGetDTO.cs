using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.DieticianOfficeDTO
{
    public class DieticianOfficesGetDTO
    {
        public int DieticianId { get; set; }
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
    }
}
