using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    /// <summary>
    /// Realizacja relacji many to many
    /// </summary>
    [Table("MessageDietetician")]
    public class MessageDietetician : BaseModel
    {
        public int DieticianId { get; set; }
        public Dietician Dietician { get; set; }

        public int MessageToId { get; set; }
        public MessageTo MessageTo { get; set; }
    }
}
