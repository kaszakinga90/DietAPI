using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    /// <summary>
    /// Pojedyncze wyniki przeprowadzonych badań analitycznych np. z krwi
    /// </summary>
    [Table("SingleTestEqual")]
    public class SingleTestEqual : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public float test1 { get; set; }
        public float test2 { get; set; }
        public float test3 { get; set; }

        public int TestEqualId { get; set; }
        public TestEqual TestEqual { get; set; }
    }
}