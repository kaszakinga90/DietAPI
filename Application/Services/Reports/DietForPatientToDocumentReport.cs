using Application.DTOs.GenericsDTO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Application.Services.Reports
{
    public class DietForPatientToDocumentReport : IReport
    {
        private readonly DietForPatientToDocumentDTO _data;

        public DietForPatientToDocumentReport(DietForPatientToDocumentDTO data)
        {
            _data = data;
        }

        public string GenerateReport()
        {
            var reportContent = new StringBuilder();


            Console.WriteLine(string.Empty);
            Console.WriteLine("***********************************");
            Console.WriteLine(reportContent.ToString());
            reportContent.AppendLine("Diet for patient PDF content:\n");

            string jsonReport = JsonConvert.SerializeObject(_data, Formatting.Indented);


            Console.WriteLine(reportContent.ToString());
            Debug.WriteLine(reportContent.ToString());
            return jsonReport;
        }
    }
}
