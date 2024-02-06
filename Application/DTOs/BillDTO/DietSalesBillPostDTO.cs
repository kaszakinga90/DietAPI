namespace Application.DTOs.BillDTO
{
    public class DietSalesBillPostDTO
    {
        public int Id { get; set; }
        public int DieticianId { get; set; }
        public int PatientId { get; set; }
        public int SalesId { get; set; }
        public SalesPostDTO Sales { get; set; }
    }
}