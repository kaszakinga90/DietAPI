﻿using ModelsDB;

namespace Application.DTOs.AdminDTO
{
    public class AdminUpdateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
    }
}
