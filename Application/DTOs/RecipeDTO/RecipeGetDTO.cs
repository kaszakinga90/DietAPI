using Application.DTOs.RecipeStepDTO;

namespace Application.DTOs.RecipeDTO
{
    public class RecipeGetDTO
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public List<RecipeStepGetDTO> Steps { get; set; }
    }
}
