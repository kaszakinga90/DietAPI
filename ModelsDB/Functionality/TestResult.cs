using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("TestResult")]
    public class TestResult : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int DieticianId { get; set; }
        public SingleTestResults SingleTestResults { get; set; }
        public int SingleTestResultsId { get; set; }
        public int PatientCardId { get; set; }
        public PatientCard PatientCard { get; set; }
    }
}