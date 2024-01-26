using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("DietPatient")]
    public class DietPatient : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DietId { get; set; }
        public int DieticianId { get; set; }
    }
}