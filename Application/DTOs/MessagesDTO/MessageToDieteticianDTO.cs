﻿using ModelsDB;

namespace Application.DTOs.MessagesDTO
{
    public class MessageToDieteticianDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DieticianId { get; set; }
        public int PatientId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }
        public DateTime dateAdded { get; set; } = DateTime.Now;
    }
}
