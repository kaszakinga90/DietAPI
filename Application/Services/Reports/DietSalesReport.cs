using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using Application.DTOs.ReportsClassesDTO.Reports;

namespace Application.Services.Reports
{
    public class DietSalesReport : IReport
    {
        private readonly List<DietSalesDTO> _dataList;

        public DietSalesReport(List<DietSalesDTO> dataList)
        {
            _dataList = dataList;
        }

        public string GenerateReport()
        {
            var reportContent = new StringBuilder();

            Console.WriteLine(string.Empty);
            Console.WriteLine("***********************************");
            Console.WriteLine(reportContent.ToString());
            reportContent.AppendLine("Diet sales report content:\n");

            string jsonReport = JsonConvert.SerializeObject(_dataList, Formatting.Indented);

            Console.WriteLine(reportContent.ToString());
            Debug.WriteLine(reportContent.ToString());
            return jsonReport;
        }
    }
}