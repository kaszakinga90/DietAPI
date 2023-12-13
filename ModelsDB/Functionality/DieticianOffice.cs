using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("DieticianOffice")]
    public class DieticianOffice
    {
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
    }
}
