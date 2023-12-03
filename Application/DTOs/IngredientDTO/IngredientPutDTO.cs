﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.IngredientDTO
{
    public class IngredientPutDTO
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public float Calories { get; set; }
        public int GlycemicIndex { get; set; }
        public float ServingQuantity { get; set; }
        public int MeasureId { get; set; }
        //public IFormFile File { get; set; }
        public string PictureUrl { get; set; }
        public float? Weight { get; set; }
    }
}