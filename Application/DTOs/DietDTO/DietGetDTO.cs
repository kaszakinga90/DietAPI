using Application.DTOs.MealTimeToXYAxisDTO;

public class DietGetDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PatientId { get; set; }
    public int DieteticianId { get; set; }
    public int numberOfMeals { get; set; }
    public string PatientName { get; set; }
    public string DieteticanName { get; set; }
    //public List<MealTimeToXYAxisDTO> MealTimesToXYAxisDTO { get; set; }
}