using ModelsDB.Functionality;
using ModelsDB;

namespace Application.DTOs.PatientCardDTO
{
    public class PatientCardGetDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int SexId { get; set; }
        public int DieticianId { get; set; }
        public List<PatientCardSurvey> PatientCardSurveys { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}
