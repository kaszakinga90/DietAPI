using Application.DTOs.RecipeStepDTO;

namespace Application.DTOs.RecipeDTO
{
    public class RecipePostDTO
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public List<RecipeStepPostDTO> Steps { get; set; }
    }
}
