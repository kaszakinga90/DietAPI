using ModelsDB.Functionality;
using System.Text.Json.Serialization;

namespace Application.DTOs.DayWeekDTO
{
    public class DayWeekDTO
    {
        public int Id { get; set; }
        public string Day { get; set; }

        [JsonIgnore]
        public List<SingleDiet> SingleDiets { get; set; }
    }
}
