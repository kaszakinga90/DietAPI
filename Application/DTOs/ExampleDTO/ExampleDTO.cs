using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ExampleDTO
{
    public class ExampleDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public IFormFile File { get; set; }
    }

}
