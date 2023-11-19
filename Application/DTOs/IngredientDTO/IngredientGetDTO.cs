namespace Application.DTOs.IngredientDTO
{
    public class IngredientGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Micronutrient { get; set; }
        public float Macronutrient { get; set; }
        public float Calories { get; set; }
        public float Glycemic { get; set; }
        public float Quantity { get; set; }
        public string PictureUrl { get; set; }
    }
}
