using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    public class MessageToDietician:BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Dietician Dietician { get; set; }
        public int DieticianId { get; set; }
    }
}
