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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int numberOfMeals { get; set; }
        public List<MealSchedule> MealSchedules { get; set; }
        public List<MealTimeToXYAxis> MealTimesToXYAxis { get; set; }

        public int DieteticianId { get; set; }
        public Dietician Dietician { get; set; }
        //public List<DietPatient> DietPatients { get; set; }
    }
}