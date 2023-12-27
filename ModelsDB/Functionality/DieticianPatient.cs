using System.ComponentModel.DataAnnotations;

namespace ModelsDB.Functionality
{
    public class DieticianPatient
    {
        // TODO : pole Id do usunięcia
        //[Key]
        //public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }
        public int DietId { get; set; }
        public Diet Diet { get; set; }
    }
}

