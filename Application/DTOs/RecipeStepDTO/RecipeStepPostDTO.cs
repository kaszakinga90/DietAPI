namespace Application.DTOs.RecipeStepDTO
{
    public class RecipeStepPostDTO
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }
    }
}
