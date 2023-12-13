namespace ModelsDB.Functionality
{
    public class PatientCardSurvey
    {
        public int PatientCardId { get; set; }
        public PatientCard PatientCard { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
