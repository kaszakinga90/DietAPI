using ModelsDB.ManualPanel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Layout
{
    [Table("LayoutCategory")]
    public class LayoutCategory : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public MainNavbar MainNavbar { get; set; }
        public List<Link> Links { get; set; }
    }
}
