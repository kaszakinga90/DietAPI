using ModelsDB.Functionality;

namespace Application.DTOs.DishDTO
{
    public class DishDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // TODO: czy tu nie będzie na DTO?
        public List<MealTimeToXYAxis> MealTimesToXYAxis { get; set; }
    }
}
