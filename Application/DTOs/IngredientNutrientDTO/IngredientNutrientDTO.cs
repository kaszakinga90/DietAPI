
    public class IngredientNutrientDTO
    {
        public int Id { get; set; }

        public int IngredientId { get; set; }

        public int NutrientId { get; set; }

        // wartość nutrient wchodzącego w skład ingredient (czyli np. ile w serze jest witaminy D)
        public float NutrientValue { get; set; }
    }

