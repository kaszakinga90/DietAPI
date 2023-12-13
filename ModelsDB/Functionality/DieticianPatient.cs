using System.ComponentModel.DataAnnotations;

namespace ModelsDB.Functionality
{
    public class DieticianPatient
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }
    }
}

