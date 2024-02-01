namespace Application.DTOs.DishDetailsToEditDTO
{
    public class DishRecipeDetailsGetEditDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public List<RecipeStepGetEditDTO> RecipeStepsDTO { get; set; }
    }
}