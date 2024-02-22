namespace Application.DTOs.DishFoodCatalogDTO
{
    public class DishFoodCatalogGetDTO
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public string DishName { get; set; }
        public int FoodCatalogId { get; set; }
        public string FoodCatalogName { get; set; }
    }
}
