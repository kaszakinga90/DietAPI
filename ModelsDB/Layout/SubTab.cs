﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Layout
{
    [Table("SubTab")]
    public class SubTab : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public Link Link { get; set; }
        public int LinkId { get; set; }
        public Tab Tab { get; set; }
        public int TabId { get; set; }
    }
}
