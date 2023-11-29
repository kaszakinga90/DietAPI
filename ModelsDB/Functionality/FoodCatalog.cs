using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("FoodCatalog")]
    public class FoodCatalog : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string CatalogName { get; set; }
        public List<DishFoodCatalog> DishFoodCatalogs { get; set; }
        public int? DieteticianId { get; set; }
        public Dietician Dietician { get; set; }
    }
}