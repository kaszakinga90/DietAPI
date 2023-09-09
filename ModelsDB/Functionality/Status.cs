﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("Status")]
    public class Status : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Visit> Visits { get; set; }
    }
}