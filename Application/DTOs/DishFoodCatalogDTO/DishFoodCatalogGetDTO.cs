using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.DishFoodCatalogDTO
{
    public class DishFoodCatalogGetDTO
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public string DishName { get; set; }
        public int FoodCatalogId { get; set; }
        public string FoodCatalogName { get; set; }
    }
}
