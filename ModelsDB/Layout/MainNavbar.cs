using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Layout
{
    [Table("Navbar")]
    public class MainNavbar : BaseModel
    {
        [Key]
        public int Id { get; set; }
        
        public LayoutCategory LayoutCategory { get; set; }
        public int LayoutCategoryId { get; set; }
        public List<Tab> Tabs { get; set; }
    }
}
