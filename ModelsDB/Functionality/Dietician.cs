using ModelsDB.Functionality;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB
{
    [Table("Dietician")]
    public class Dietician : User
    {
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        public List<Message> Messages { get; set; }
        public List<Diploma> Diplomas { get; set; }
        public List<Patient> Patients { get; set; }
        public List<FoodCatalog> FoodCatalogs { get; set; }
        public List<Office> Offices { get; set; }
        public List<Visit> Visits { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Diet> Diets { get; set; }
    }
}
