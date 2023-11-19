using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    /// <summary>
    /// Klasa reprezentuje pojedynczy składnik produktu (mikro lub makroelement) np. potas, witD
    /// </summary>
    [Table("Nutrient")]
    public class Nutrient : BaseModel
    {
        [Key]
        public int Id { get; set; }

        //czyli "attr_id"
        public int? NutritionixId { get; set; }
        public string NamePL { get; set; }
        public string NameEN { get; set; }
        public bool IsMacronutrient { get; set; }
        public bool IsMicronutrient { get; set; }

        // która to jest jednostka, np. gramy
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public List<IngredientNutrient> Ingredients { get; set; }
    }
}
