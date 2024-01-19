using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("ReportTemplate")]
    public class ReportTemplate : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isContainHeader { get; set; }
        public bool isContainFooter { get; set; }
    }
}