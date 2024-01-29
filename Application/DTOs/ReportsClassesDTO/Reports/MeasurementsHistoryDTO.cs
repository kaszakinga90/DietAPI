using Application.BusinessLogic.CalculatesAndStatistics;

namespace Application.DTOs.ReportsClassesDTO.Reports
{
    public class MeasurementsHistoryDTO
    {
        public int Id { get; set; }
        public string MeasureTime { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public float Bmi { get; set; }
        public float WeightChange { get; set; }
        public float BmiChange { get; set; }

        public void CalculateBMI()
        {
            var bmiCalculator = new CalculatorService();
            Bmi = bmiCalculator.CalculateBMI(Weight, Height);
        }
    }
}