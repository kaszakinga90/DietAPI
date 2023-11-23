using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Diploma")]
    public class Diploma : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }
        public string PublicId { get; set; }
        public string PictureUrl { get; set; }
    }
}