using Application.DTOs.ReportsClassesDTO.Reports;

namespace Application.BusinessLogic.CalculatesAndStatistics
{
    public class CalculatorService
    {
        public float CalculateBMI(float weightKg, float heightCm)
        {
            float heightM = heightCm / 100;
            float bmi = weightKg / (heightM * heightM);

            return (float)Math.Round(bmi, 2);
        }

        public void CalculateWeightChange(List<MeasurementsHistoryDTO> measurements)
        {
            if (measurements == null || measurements.Count < 2)
            {
                return;
            }

            for (int i = 1; i < measurements.Count; i++)
            {
                float previousWeight = measurements[i - 1].Weight;
                float currentWeight = measurements[i].Weight;

                float change = currentWeight - previousWeight;
                change = (float)Math.Round(change, 2);
                measurements[i].WeightChange = change;
            }
        }

        public void CalculateBMIChange(List<MeasurementsHistoryDTO> measurements)
        {
            if (measurements == null || measurements.Count < 2)
            {
                return;
            }

            for (int i = 1; i < measurements.Count; i++)
            {
                float previousBMI = measurements[i - 1].BMI;
                float currentBMI = measurements[i].BMI;

                float change = currentBMI - previousBMI;
                change = (float)Math.Round(change, 2);
                measurements[i].BMIChange = change;
            }
        }
    }
}