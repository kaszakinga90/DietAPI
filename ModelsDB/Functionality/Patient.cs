using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("Patient")]
    public class Patient : User
    {
        public Dietician Dietician { get; set; }
        public int DieticianId { get; set; }
        public PatientCard PatientCard { get; set; }
        public int PatientCardId { get; set; }
        public List<Diet> Diets { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Visit> Visits { get; set; }
        public List<Message> Messages { get; set; }
    }
}