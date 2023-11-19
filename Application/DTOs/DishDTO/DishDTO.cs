using ModelsDB.Functionality;

namespace Application.DTOs.DishDTO
{
    public class DishDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MealSchedule> MealSchedules { get; set; }
    }
}
