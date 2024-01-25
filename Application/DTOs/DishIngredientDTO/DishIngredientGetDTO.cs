namespace Application.DTOs.DishIngredientDTO
{
    public class DishIngredientGetDTO
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }
    }
}