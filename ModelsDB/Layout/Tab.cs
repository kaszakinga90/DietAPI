using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Layout
{
    [Table("Tab")]
    public class Tab : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public List<MainNavbar> MainNavbars { get; set; }
        public List<SubTab> SubTabs { get; set; }
    }
}
