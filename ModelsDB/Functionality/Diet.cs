using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Diet")]
    public class Diet : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<DietSingleDiet> DietSingleDiets { get; set; }
        public int DieteticianId { get; set; }
        public Dietician Dietician { get; set; }
        public List<DietPatient> DietPatients { get; set; }
    }
}