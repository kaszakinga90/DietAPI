namespace Application.DTOs.NutrientDTO
{
    public class NutrientDTO
    {
        public int Id { get; set; }

        //czyli "attr_id"
        public int? NutritionixId { get; set; }
        public string NamePL { get; set; }
        public string NameEN { get; set; }
        public bool IsMacronutrient { get; set; }
        public bool IsMicronutrient { get; set; }

        // która to jest jednostka, np. gramy
        public int UnitId { get; set; }
        //public List<IngredientNutrient> Ingredients { get; set; }
    }
}
