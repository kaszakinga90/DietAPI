using Application.DTOs.RecipeStepDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RecipeDTO
{
    public class RecipeGetDTO
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public List<RecipeStepGetDTO> Steps { get; set; }
    }
}
