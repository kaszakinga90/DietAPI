using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("MealTimeToXYAxis")]
    public class MealTimeToXYAxis : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime MealTime { get; set; }
        public int? DietId { get; set; }
        public Diet Diet { get; set; }
    }
}