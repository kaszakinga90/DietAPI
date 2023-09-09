using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.ManualPanel
{
    [Table("Content")]
    public class Content : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public string PhotoURL { get; set; }

        public List<Manual> Manuals { get; set; }
        public List<Document> Documents { get; set; }
    }
}
