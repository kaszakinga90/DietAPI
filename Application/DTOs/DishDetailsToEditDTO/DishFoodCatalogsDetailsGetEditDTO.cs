namespace Application.DTOs.DishDetailsToEditDTO
{
    public class DishFoodCatalogsDetailsGetEditDTO
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int FoodCatalogId { get; set; }
        public string FoodCatalogName { get; set; }
    }
}