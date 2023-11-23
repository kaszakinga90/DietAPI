using Microsoft.AspNetCore.Http;
using ModelsDB;

namespace Application.DTOs.DiplomaDTO
{
    public class DiplomaPostDTO
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DieticianId { get; set; }
        public IFormFile File { get; set; }

    }
}
