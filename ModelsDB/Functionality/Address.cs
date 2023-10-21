using ModelsDB.Layout;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelsDB
{
    [Table("Address")]
    public class Address : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string LocalNo { get; set; }

        [JsonIgnore]
        public List<Dietician> Dieticians { get; set; }
        [JsonIgnore]
        public List<Patient> Patients { get; set; }
        [JsonIgnore]
        public List<Office> Offices { get; set; }

    }
}