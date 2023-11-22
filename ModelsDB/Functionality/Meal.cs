using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("Meal")]
    public class Meal : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MealTimeToXYAxis> MealTimeToXYAxes { get; set; }
    }
}
