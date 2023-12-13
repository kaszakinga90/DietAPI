using Microsoft.AspNetCore.Http;

namespace Application.DTOs.DiplomaDTO
{
    public class DiplomaPostDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DieticianId { get; set; }
        public IFormFile File { get; set; }
    }
}
