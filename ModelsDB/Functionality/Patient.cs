using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations.Schema;

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
        public List<MessagePatient> MessagePatients { get; set; }
        public List<DieticianPatient> DieticianPatients { get; set; }
        public List<DietPatient> DietPatients { get; set; }
        public List<NotePatient> NotePatients { get; set; }
        public List<MessageToPatient> MessageToPatients { get; set; }
    }
}