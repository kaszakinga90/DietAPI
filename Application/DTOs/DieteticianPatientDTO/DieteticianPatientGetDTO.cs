namespace Application.DTOs.DieteticianPatientDTO
{
    public class DieteticianPatientGetDTO
    {
        public int PatientId { get; set; }
        public int DieticianId { get; set; }
        public string DieteticianName { get; set; }
        public string PatientName { get; set; }
    }
}
