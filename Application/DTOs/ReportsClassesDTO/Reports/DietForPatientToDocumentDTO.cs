namespace Application.DTOs.ReportsClassesDTO.Reports
{
    public class DietForPatientToDocumentDTO
    {
        public string Name { get; set; }
        public string PatientName { get; set; }
        public string DieticianName { get; set; }
        public string DieticianLogoUrl { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int numberOfMeals { get; set; }
        public int Period { get; set; }
        public List<MealTimeToXYAxisToReportDTO> MealTimesToXYAxisDTO { get; set; }

    }
}
