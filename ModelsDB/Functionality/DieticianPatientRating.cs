using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    //[Table("DieticianPatientRating")]
    public class DieticianPatientRating 
    {
        public int DieticianId { get; set; }
        public int PatientId { get; set; }
        [Key]
        public int RatingId { get; set; }
        public Dietician Dietician { get; set; }
        public Patient Patient { get; set; }
        public Rating Rating { get; set; }
    }
}
