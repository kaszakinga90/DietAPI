﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Survey")]
    public class Survey : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public float Heigth { get; set; }
        public float Weith { get; set; }
        public DateTime MeasureTime { get; set; }

        public int PatientCardId { get; set; }
        public PatientCard PatientCard { get; set; }
    }
}