using Application.DTOs.MealTimeToXYAxisDTO;

namespace Application.DTOs.DietDTO
{
    public class DietDetailsGetDTO
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PatientName { get; set; }
        public string DieticianName { get; set; }
        public int numberOfMeals { get; set; }
        public List<MealTimeToDietDetailsGetDTO> MealTimeToDietDetailsGetDTO { get; set; }
    }
}