using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("ReportTemplatePreview")]
    public class ReportTemplatePreview : BaseModel
    {
        public int Id { get; set; }
        public string UrlReportTemplate1 { get; set; }
        public string UrlReportTemplate2 { get; set; }
        public string UrlReportTemplate3 { get; set; }
        public string UrlReportTemplate4 { get; set; }
    }
}