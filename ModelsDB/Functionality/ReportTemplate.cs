﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("ReportTemplate")]
    public class ReportTemplate : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        // TODO : - jaka struktura dla ReportTemplate ?
        //public bool? Content { get; set; }
        //public bool? Table { get; set; }
    }
}
