using ModelsDB.Functionality;

namespace ModelsDB
{

    public class Patient : User
    {
        public List<Dietician> Dieticians { get; set; }
        public PatientCard PatientCard { get; set; }
        public int PatientCardId { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Visit> Visits { get; set; }
        public List<DietPatient> DietPatients { get; set; }
        public List<NotePatient> NotePatients { get; set; }

        //lista odzwierc. jeden pacjent może mieć wiele dietetyków
        // jeden dietetyk może mieć wiele pacjentów
        public List<DieticianPatient> DieticianPatients { get; set; }

        public List<MessageTo> MessageTo { get; set; }
        public List<MessagePatient> MessagePatients { get; set; }
    }
}