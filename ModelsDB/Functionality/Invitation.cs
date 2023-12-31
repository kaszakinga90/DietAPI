using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("Invitation")]
    public class Invitation : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public Dietician Dietician { get; set; }
        public int DieticianId { get; set; }
        public bool IsAccepted { get; set; } = false;
        public bool IsSended { get; set; } = false;
        public bool IsDeclined { get; set; } = false;
        //  0 - false, 1 - true
    }
}
