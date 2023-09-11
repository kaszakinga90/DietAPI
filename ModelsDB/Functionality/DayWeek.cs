using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelsDB.Functionality
{
    [Table("DayWeek")]
    public class DayWeek : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Day { get; set; }

        [JsonIgnore]
        public List<SingleDiet> SingleDiets { get; set; }
    }
}