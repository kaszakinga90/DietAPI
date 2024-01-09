﻿using Microsoft.AspNetCore.Identity;

namespace ModelsDB.Functionality
{
    public class Role : IdentityRole<int>
    {
        public bool isActive { get; set; } = true;
        public DateTime dateAdded { get; set; } = DateTime.Now;
        public DateTime? dateUpdated { get; set; } = null;  // Nullowalna data
        public DateTime? dateDeleted { get; set; } = null;  // Nullowalna data
        public string whoAdded { get; set; }
        public string whoUpdated { get; set; }
        public string whoDeleted { get; set; }
    }
}
