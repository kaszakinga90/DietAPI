using Application.DTOs.GenericsDTO;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

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

            //foreach (var data in _dataList)
            //{
            //    reportContent.AppendLine($"Diet Name: {data.DietName}");
            //    reportContent.AppendLine($"CreateTime: {data.CreateTime}");
            //    reportContent.AppendLine($"Patient Name: {data.PatientName}");
            //    reportContent.AppendLine($"Period: {data.Period}");
            //    reportContent.AppendLine("------------------------------");

            //}

            string jsonReport = JsonConvert.SerializeObject(_dataList, Formatting.Indented);


            Console.WriteLine(reportContent.ToString());
            Debug.WriteLine(reportContent.ToString());
            return jsonReport;
        }
    }
}
