using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("Visit")]
    public class Visit : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateVisit { get; set; }
        public  int TermId { get; set; }
        public Term Term { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
    }
}
