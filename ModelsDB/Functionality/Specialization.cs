namespace ModelsDB.Functionality
{
    public class Specialization : BaseModel
    {
        public int Id { get; set; }
        public string SpecializationName { get; set; }
        public List<DieticianSpecialization> DieticianSpecializations { get; set; }
    }
}
