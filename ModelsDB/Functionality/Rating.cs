using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Rating")]
    public class Rating : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int Note { get; set; }

        public List<Dietician> Dieticians { get; set; }
    }
}