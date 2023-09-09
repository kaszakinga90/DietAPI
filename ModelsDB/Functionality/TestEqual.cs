using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("TestEqual")]
    public class TestEqual : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime TestDate { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public List<SingleTestEqual> SingleTestEqual { get; set; }
        public int PatientCardId { get; set; }
        public PatientCard PatientCard { get; set; }
    }
}