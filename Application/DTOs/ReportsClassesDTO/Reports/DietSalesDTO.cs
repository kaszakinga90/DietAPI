namespace Application.DTOs.ReportsClassesDTO.Reports
{
    public class DietSalesDTO
    {
        public int Id { get; set; }
        public string DietName { get; set; }
        public DateTime CreateTime { get; set; }
        public string PatientName { get; set; }
        public int Period { get; set; }
        public string Price { get; set; }
    }
}
