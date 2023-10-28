using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    public class MessageToPatient:BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public Dietician Dietician { get; set; }
        public int DieticianId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }
    }
}
