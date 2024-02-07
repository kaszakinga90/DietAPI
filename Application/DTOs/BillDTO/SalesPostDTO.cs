namespace Application.DTOs.BillDTO
{
    public class SalesPostDTO
    {
        public int Id { get; set; }
        public int DietId { get; set; }
        public double Price { get; set; }
        public bool IsPaid { get; set; }
        public DateTime SalesDate { get; set; } = DateTime.Now;
    }
}