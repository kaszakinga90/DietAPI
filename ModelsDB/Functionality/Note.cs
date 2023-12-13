using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Note")]
    public class Note : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isVisibleToPatient { get; set; }
        public int DieticianId { get; set; }
        public List<DieticianNote> DieticianNotes { get; set; }
        public List<NotePatient> NotePatients { get; set; }
    }
}