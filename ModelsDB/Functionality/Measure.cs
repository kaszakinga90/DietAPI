using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Measure")]
    public class Measure : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Dish> Dishes { get; set; }
    }
}