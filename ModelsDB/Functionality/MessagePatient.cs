using ModelsDB;
using ModelsDB.Functionality;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Realizacja relacji many to many
/// </summary>
[Table("MessagePatient")]
public class MessagePatient : BaseModel
{
    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public int MessageToId { get; set; }
    public MessageTo MessageTo { get; set; }
}
