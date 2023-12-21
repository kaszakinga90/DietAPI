using Application.DTOs.DishDTO;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.DTOs.RecipeDTO;

namespace Application.DTOs.GenericsDTO
{
    public class DietForPatientToDocumentDTO
    {
        public string Name { get; set; }
        public string PatientName { get; set; }
        public string DieticianName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int numberOfMeals { get; set; }
        public int Period { get; set; }
        public List<MealTimeToXYAxisToReportDTO> MealTimesToXYAxisDTO { get; set; }
        public List<DishGetDTO> DishesDTO { get; set; }
        public List<RecipeGetDTO> RecipesDTO { get; set;}


        //logo dietetyka
        //rozpiska diety czyli mealtimetoxyaxis
        // + dania, rpzepisy, składniki
        //liczba kalorii per przepis
        //liczba kalorii na cały dzień

    }
}
