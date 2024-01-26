using Application.DTOs.MealTimeToXYAxisDTO;

namespace Application.DTOs.DietDTO
{
    public class DietDetailsGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PatientName { get; set; }
        public string DieticianName { get; set; }
        public int numberOfMeals { get; set; }
        public List<MealTimeToDietDetailsGetDTO> MealTimeToDietDetailsGetDTO { get; set; }
    }
}