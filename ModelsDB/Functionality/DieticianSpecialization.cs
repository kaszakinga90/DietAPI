namespace ModelsDB.Functionality
{
    public class DieticianSpecialization : BaseModel
    {
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
