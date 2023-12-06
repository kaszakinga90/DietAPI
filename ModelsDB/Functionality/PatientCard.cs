using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("PatientCard")]
    public class PatientCard : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public int SexId { get; set; }
        public Sex Sex { get; set; }
        public List<PatientCardSurvey> PatientCardSurveys { get; set; }
        public List<TestEqual> TestEquals { get; set; }
    }
}