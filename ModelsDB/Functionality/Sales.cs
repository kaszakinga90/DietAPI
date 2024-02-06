using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    [Table("Sales")]
    public class Sales : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int DietId { get; set; }
        public double Price { get; set; }
        public bool IsPaid { get; set; }
        public DateTime SalesDate { get; set; } = DateTime.Now;
    }
}
