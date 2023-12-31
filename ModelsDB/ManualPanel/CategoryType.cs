﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.ManualPanel
{
    [Table("CategoryType")]
    public class CategoryType : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FileCategory> FileCategories { get; set; }
    }
}
