using ModelsDB.Functionality;

namespace ModelsDB
{

    public class Patient : User
    {
        //public List<Dietician> Dieticians { get; set; }
        public List<PatientCard> PatientCards { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Visit> Visits { get; set; }
        //public List<DietPatient> DietPatients { get; set; }
        public List<NotePatient> NotePatients { get; set; }
        public List<DieticianPatient> DieticianPatients { get; set; }
        public List<MessageTo> MessageTo { get; set; }
        public List<Diet> Diets { get; set; }
        public List<TestResult> TestEquals { get; set; }
        public List<DieticianPatientRating> DieticianRatings { get; set; }

    }
}