using System.ComponentModel.DataAnnotations;

namespace ModelsDB.Functionality
{
    public class DieticianPatientRating : BaseModel
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
