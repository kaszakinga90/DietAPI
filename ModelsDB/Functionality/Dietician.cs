using ModelsDB.Functionality;
namespace ModelsDB
{

    public class Dietician : User
    {

        public Rating Rating { get; set; }
        public int RatingId { get; set; }
        public List<DieticianMessage> MessageDieticians { get; set; }
        public List<Diploma> Diplomas { get; set; }
        public List<DieticianPatient> DieticianPatients { get; set; }
        public List<FoodCatalog> FoodCatalogs { get; set; }
        public List<DieticianOffice> DieticianOffices { get; set; }
        public List<Visit> Visits { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Diet> Diets { get; set; }
        public List<DieticianNote> DieticianNotes { get; set; }
        public List<MessageToDietician> MessageToDieticians { get; set; }
    }
}
