using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("DishFoodCatalog")]
    public class DishFoodCatalog : BaseModel
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public int FoodCatalogId { get; set; }
        public FoodCatalog FoodCatalog { get; set; }
    }
}
