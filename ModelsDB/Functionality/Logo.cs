using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("Logo")]
    public class Logo : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }
        public string PublicId { get; set; }
        public string PictureUrl { get; set; }
    }
}
