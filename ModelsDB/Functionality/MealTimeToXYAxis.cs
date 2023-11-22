using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("MealTimeToXYAxis")]
    public class MealTimeToXYAxis : BaseModel
    {
        [Key]
        public int Id { get; set; }

        // tutaj będzie nazwa posilku (sniadanie, obiad, kolacja, itp.)
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public DateTime MealTime { get; set; }
        public int? DietId { get; set; }
        public int? DishId { get; set; }
        public Diet Diet { get; set; }
        public Dish Dish { get; set; }
    }
}