using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    /// <summary>
    /// Tabela reprezentująca jednostki miary dla produktów (ich części) jak np. gramy, kalorie, mg
    /// </summary>
    [Table("Unit")]
    public class Unit : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set;}
        public List<Nutrient> Nutrients { get; set;}
        public List<DishIngredient> DishIngredients { get; set;}
    }
}
