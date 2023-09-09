﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Layout
{
    [Table("LayoutPhoto")]
    public class LayoutPhoto : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string PhotoURL { get; set; }
        public string Description { get; set; }
        public List<Article> Articles { get; set; }
        public List<Carousel> Carousels { get; set; }
        public List<News> Newses { get; set; }
    }
}
