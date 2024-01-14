namespace Application.DTOs.Surveys
{
    public class SurveyPostDTO
    {
        public int Id { get; set; }
        public float Heigth { get; set; }
        public float Weith { get; set; }
        public DateTime MeasureTime { get; set; }
        public int DieticianId { get; set; }
        public int PatientCardId { get; set; }
    }
}
