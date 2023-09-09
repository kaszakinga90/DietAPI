﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Layout
{
    [Table("SocialMedia")]
    public class SocialMedia : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public Link Link { get; set; }
        public int LinkId { get; set; }
        public Footer Footer { get; set; }
        public int FooterId { get; set; }
    }
}
