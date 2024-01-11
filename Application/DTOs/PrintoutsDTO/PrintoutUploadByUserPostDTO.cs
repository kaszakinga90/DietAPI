using Microsoft.AspNetCore.Http;

namespace Application.DTOs.PrintoutsDTO
{
    public class PrintoutUploadByUserPostDTO
    {
        public IFormFile TemplateFile { get; set; }
        public int DieticianId { get; set; }
    }
}
