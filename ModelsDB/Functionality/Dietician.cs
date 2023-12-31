using ModelsDB.Functionality;
namespace ModelsDB
{
    public class Dietician : User
    {
        public string AboutMe { get; set; }
        public Logo Logo { get; set; }
        public List<Diploma> Diplomas { get; set; } 
        public List<FoodCatalog> FoodCatalogs { get; set; }
        public List<DieticianOffice> DieticianOffices { get; set; }
        public List<Visit> Visits { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Diet> Diets { get; set; }
        public List<DieticianNote> DieticianNotes { get; set; }
        public List<DieticianPatient> DieticianPatients { get; set; }
        public List<MessageTo> MessageTo { get; set; }
        public List<DieticianSpecialization> DieticianSpecializations { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<DieticianPatientRating> DieticianRatings { get; set; }
        public List<Invitation> Invitations { get; set; }
    }
}