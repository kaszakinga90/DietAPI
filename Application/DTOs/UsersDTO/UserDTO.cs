﻿namespace Application.DTOs.UsersDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsPatient { get; set; }
        public bool IsDietician { get; set; }
        public bool IsAdmin { get; set; }
        public bool isSuperAdmin { get; set; }
    }
}
