using DietDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsDB;

namespace API.Controllers
{
    public class ExampleController : BaseApiController
    {
        private readonly DietContext _context;

        public ExampleController(DietContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Example>>> GetExamples()
        {
            return await _context.Examples.ToListAsync();
        }
    }
}
