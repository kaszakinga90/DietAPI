using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("DishIngredient")]
    public class DishIngredient
    {
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public int UnitId { get; set; }
        public int DishId { get; set; }
        public Dish Dish { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
