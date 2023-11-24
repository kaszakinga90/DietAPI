using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    /// <summary>
    /// Klasa reprezentuje pojedynczy produkt (składnik posiłku) np. ser biały, mleko
    /// </summary>
    [Table("Ingredient")]
    public class Ingredient : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public float Calories { get; set; }

        //odpowiednik "serving_qty"  odp. np 1
        public float? ServingQuantity { get; set; }
        //odpowiednik "serving_unit"  odp. np. "slice (1 oz)"
        public int MeasureId { get; set; }
        public Measure Measure { get; set; }

        //odpowiednik "serving_weight_grams"  odp. np 28
        public float? Weight { get; set; }

        // która to jest jednostka, np. gramy
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public int? DieticianId { get; set; } = null;
        public Dietician Dietician { get; set; }

        public int? GlycemicIndex { get; set; }
        public string PublicId { get; set; }
        public string PictureUrl { get; set; }
        public List<IngredientNutrient> Nutrients { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
    }
}