using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("TestResult")]
    public class TestResult : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime TestDate { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public List<SingleTestResults> SingleTestEqual { get; set; }
        public int PatientCardId { get; set; }
        public PatientCard PatientCard { get; set; }
    }
}