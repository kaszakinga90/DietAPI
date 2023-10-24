using ModelsDB;
using System.ComponentModel.DataAnnotations.Schema;

[Table("MessagePatient")]
public class MessagePatient
{
    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public int MessageId { get; set; }
    public Message Message { get; set; }
}
