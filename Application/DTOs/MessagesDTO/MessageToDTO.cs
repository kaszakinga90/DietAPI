public class MessageToDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int AdminId { get; set; }
    public int DieticianId { get; set; }
    public int PatientId { get; set; }
    public string AdminName { get; set; }
    public string DieticianName { get; set; }
    public string PatientName { get; set; }
    public bool IsRead { get; set; }
    public bool IsActive { get; set; }
    public DateTime? ReadDate { get; set; }
    public DateTime dateAdded { get; set; } = DateTime.Now;
}
