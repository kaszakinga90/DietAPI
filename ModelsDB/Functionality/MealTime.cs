using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("MealTime")]
    public class MealTime : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string DishTime { get; set; }

        public List<MealTimeSingleDiet> MealTimeSingleDiets { get; set; }
    }
}