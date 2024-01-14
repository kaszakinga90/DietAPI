namespace Application.DTOs.PatientsCardsSurveys
{
    public class PatientCardSurveyGetDTO
    {
        public int PatientCardId { get; set; }
        public int SurveyId { get; set; }
        public int DieticianId { get; set; }
        public DateTime MeasureTime { get; set; }
        public float Heigth { get; set; }
        public float Weith { get; set; }
    }
}
