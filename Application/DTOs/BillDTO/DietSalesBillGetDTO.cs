namespace Application.DTOs.BillDTO
{
    public class DietSalesBillGetDTO
    {
        public int Id { get; set; }
        public int DieticianId { get; set; }
        public int PatientId { get; set; }
        public int SalesId { get; set; }
        public SalesGetDTO Sales { get; set; }
        public string DieticianName { get; set; }
        public string PatientName { get; set;}
    }
}