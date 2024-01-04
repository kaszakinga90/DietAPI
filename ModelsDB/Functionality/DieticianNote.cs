using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("DieticianNote")]
    public class DieticianNote : BaseModel
    {
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
