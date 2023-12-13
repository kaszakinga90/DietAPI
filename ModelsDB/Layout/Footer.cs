using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Layout
{
    [Table("Footer")]
    public class Footer : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string LogoURL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Link> Links { get; set; }
        public List<SocialMedia> SocialMedia { get; set; }
    }
}
