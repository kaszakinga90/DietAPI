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
        public List<Dish> Dishes { get; set; }
        public int DieteticianId { get; set; }
        public Dietician Dietician { get; set; }
    }
}