using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    /// <summary>
    /// Pojedyncze wyniki przeprowadzonych badań analitycznych np. z krwi
    /// </summary>
    [Table("SingleTestResults")]
    public class SingleTestResults : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public float test1 { get; set; }
        public float test2 { get; set; }
        public float test3 { get; set; }
        public DateTime TestDate { get; set; }
        public int DieticianId { get; set; }
        public List<TestResult> TestResult { get; set; }
    }
}