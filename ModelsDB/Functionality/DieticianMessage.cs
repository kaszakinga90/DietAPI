using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    [Table("DieticianMessage")]
    public class DieticianMessage
    {
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}
