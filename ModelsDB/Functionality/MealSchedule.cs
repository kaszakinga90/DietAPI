using System.ComponentModel.DataAnnotations;

namespace ModelsDB.Functionality
{
    public class MealSchedule : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int DietId { get; set; }
        public int DishId { get; set; }
        public DateTime MealTime { get; set; } // Data i godzina posiłku

        public Diet Diet { get; set; }
        public Dish Dish { get; set; }
    }
}
