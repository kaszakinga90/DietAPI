using ModelsDB;

namespace Application.DTOs.AdminDTO
{
    public class AdminPostDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isPatient { get; set; } = false;
        public bool isDietician { get; set; } = false;
        public bool isAdmin { get; set; } = true;
        public bool isSuperAdmin { get; set; } = false;
        public string PictureUrl { get; set; }
    }
}
