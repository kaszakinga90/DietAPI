namespace ModelsDB.Functionality
{
    public class DietPatient
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DietId { get; set; }
        public Diet Diet { get; set; }
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }
    }
}
