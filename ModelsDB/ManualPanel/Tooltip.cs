using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.ManualPanel
{
    [Table("Tooltip")]
    public class Tooltip : BaseModel
    {
        [Key]
        public int Id { get; set; }
        // oznaczenie-kod
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
