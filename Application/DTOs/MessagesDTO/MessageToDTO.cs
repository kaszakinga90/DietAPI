﻿public class MessageToDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int? AdminId { get; set; } = null;
    public int? DieticianId { get; set; } = null;
    public int? PatientId { get; set; } = null;
    public string AdminName { get; set; }
    public string DieticianName { get; set; }
    public string PatientName { get; set; }
    public bool IsRead { get; set; }
    public bool IsActive { get; set; }
    public DateTime? ReadDate { get; set; }
    public DateTime dateAdded { get; set; } = DateTime.Now;
    public bool AdminSended { get; set; }
    public bool PatientSended { get; set; }
    public bool DieticianSended { get; set; }

}
