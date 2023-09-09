using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Diploma")]
    public class Diploma : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string PhotoDiplomaUrl { get; set; }
        public string PhotoDiplomaLink { get; set;}

        public int DieteticianId { get; set; }
        public Dietician Dietician { get; set; }
    }
}