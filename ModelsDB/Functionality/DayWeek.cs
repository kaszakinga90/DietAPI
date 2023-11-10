using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("DayWeek")]
    public class DayWeek : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Day { get; set; }
    }
}