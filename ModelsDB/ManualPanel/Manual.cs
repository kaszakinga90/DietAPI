using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.ManualPanel
{
    [Table("Manual")]
    public class Manual : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public FileCategory FileCategory { get; set; }
        public int FileCategoryId { get; set; }
        public Content Content { get; set; }
        public int ContentId { get; set; }
    }
}
