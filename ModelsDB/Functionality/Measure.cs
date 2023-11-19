using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    /// <summary>
    /// Tabela reprezentująca jednostki miary dla produktów (ich części) jak np. plaster, szklanka, kromka
    /// </summary>
    [Table("Measure")]
    public class Measure : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public List<DishMeasure> DishMeasures { get; set; }
    }
}