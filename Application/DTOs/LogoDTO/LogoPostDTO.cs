using Microsoft.AspNetCore.Http;

namespace Application.DTOs.LogoDTO
{
    public class LogoPostDTO
    {
        public int DieticianId { get; set; }
        public IFormFile File { get; set; }
    }
}
