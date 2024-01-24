namespace Application.DTOs.DietPatientDTO
{
    public class DietPatientPostDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DietId { get; set; }
        public int DieticianId { get; set; }
        public string DieticianName { get; set; }
        public string PatientName { get; set; }
        public string DietName { get; set; }
    }
}