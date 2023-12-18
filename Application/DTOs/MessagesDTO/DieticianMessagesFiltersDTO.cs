using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MessagesDTO
{
    public class DieticianMessagesFiltersDTO
    {
        public List<DateTime> DatesAdded { get; set; }
        public List<string> PatientNames { get; set; }
    }
}
