using ModelsDB.Functionality;
namespace ModelsDB
{
    public class Dietician : User
    {
        public Rating Rating { get; set; }
        public int RatingId { get; set; }
        public List<Diploma> Diplomas { get; set; } 
        public List<FoodCatalog> FoodCatalogs { get; set; }
        public List<DieticianOffice> DieticianOffices { get; set; }
        public List<Visit> Visits { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Diet> Diets { get; set; }
        public List<DieticianNote> DieticianNotes { get; set; }

        //lista odzwierc. jeden pacjent może mieć wiele dietetyków
        // jeden dietetyk może mieć wiele pacjentów
        public List<DieticianPatient> DieticianPatients { get; set; }
        public List<MessageTo> MessageTo { get; set; }
    }
}