using Application.BusinessLogic.CalculatesAndStatistics;

namespace Application.DTOs.ReportsClassesDTO.Reports
{
    public class MeasurementsHistoryDTO
    {
        public string MeasureTime { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public float BMI { get; set; }
        public float WeightChange { get; set; }
        public float BMIChange { get; set; }

        public void CalculateBMI()
        {
            var bmiCalculator = new CalculatorService();
            BMI = bmiCalculator.CalculateBMI(Weight, Height);
        }
    }
}