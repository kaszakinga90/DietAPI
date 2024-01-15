namespace Application.DTOs.TestsResultsDTO
{
    public class TestResultPostDTO
    {
        public int Id { get; set; }
        public float test1 { get; set; }
        public float test2 { get; set; }
        public float test3 { get; set; }
        public DateTime TestDate { get; set; }
        public int DieticianId { get; set; }
        public int PatientCardId { get; set; }
    }
}
