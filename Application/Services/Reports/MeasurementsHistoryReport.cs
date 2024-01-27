using Application.DTOs.ReportsClassesDTO.Reports;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Application.Services.Reports
{
    public class MeasurementsHistoryReport : IReport
    {
        private readonly List<MeasurementsHistoryDTO> _dataList;

        public MeasurementsHistoryReport(List<MeasurementsHistoryDTO> dataList)
        {
            _dataList = dataList;
        }

        public string GenerateReport()
        {
            var reportContent = new StringBuilder();

            Console.WriteLine(string.Empty);
            Console.WriteLine("***********************************");
            Console.WriteLine(reportContent.ToString());
            reportContent.AppendLine("Measurement history report content:\n");

            string jsonReport = JsonConvert.SerializeObject(_dataList, Formatting.Indented);

            Console.WriteLine(reportContent.ToString());
            Debug.WriteLine(reportContent.ToString());
            return jsonReport;
        }
    }
}