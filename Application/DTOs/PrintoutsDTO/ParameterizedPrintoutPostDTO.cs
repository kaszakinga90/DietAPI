using Microsoft.AspNetCore.Http;

namespace Application.DTOs.PrintoutsDTO
{
    public class ParameterizedPrintoutPostDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public IFormFile WordFile { get; set; }
    }
}
