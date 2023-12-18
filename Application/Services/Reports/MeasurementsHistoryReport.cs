using Application.DTOs.GenericsDTO;

namespace Application.Services.Reports
{
    public class MeasurementsHistoryReport : IReport
    {
        private readonly MeasurementsHistoryDTO _data;

        public MeasurementsHistoryReport(MeasurementsHistoryDTO data)
        {
            _data = data;
        }

        public string GenerateReport()
        {
            // TODO: Logika generowania raportu na podstawie danych _data
            return "Measurement report content";
        }
    }
}
