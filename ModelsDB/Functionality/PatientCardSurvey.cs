namespace ModelsDB.Functionality
{
    public class PatientCardSurvey : BaseModel
    {
        public int PatientCardId { get; set; }
        public PatientCard PatientCard { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public int DieticianId { get; set; }
    }
}
