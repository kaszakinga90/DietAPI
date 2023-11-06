using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB.Functionality
{
    /// <summary>
    /// Realizacja relacji many to many
    /// </summary>
    [Table("MessageAdmin")]
    public class MessageAdmin : BaseModel
    {
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public int MessageToId { get; set; }
        public MessageTo MessageTo { get; set; }
    }
}
