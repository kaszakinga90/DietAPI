using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("DietSalesBill")]
    public class DietSalesBill
    {
        [Key]
        public int Id { get; set; }
        public int DieticianId { get; set; }
        public int PatientId { get; set; }
        public int SalesId { get; set; }
        public Sales Sales { get; set; }
    }
}