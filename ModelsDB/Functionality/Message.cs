using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Message")]
    public class Message : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Dietician> Dieticians { get; set; }
        public List<Patient> Patients { get; set; }
    }
}