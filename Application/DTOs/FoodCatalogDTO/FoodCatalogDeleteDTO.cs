namespace Application.DTOs.FoodCatalogDTO
{
    public class FoodCatalogDeleteDTO
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public int? DieticianId { get; set; }
    }
}