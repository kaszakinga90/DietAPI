namespace Application.DTOs.DishDetailsToEditDTO
{
    public class DishIngredientsDetailsGetEditDTO
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }
    }
}