namespace ModelsDB.Functionality
{
    public class NotePatient : BaseModel
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
