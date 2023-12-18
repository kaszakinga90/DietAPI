using Application.DTOs.GenericsDTO;

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
            // TODO: Logika generowania raportu na podstawie danych _data
            return "Diet for patient report content";
        }
    }
}
