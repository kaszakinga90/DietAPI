namespace ModelsDB.Functionality
{
    public class DieticianPatient
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }
    }
}

