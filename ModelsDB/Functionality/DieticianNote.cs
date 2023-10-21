using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    [Table("DieticianNote")]
    public class DieticianNote
    {
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }

        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
