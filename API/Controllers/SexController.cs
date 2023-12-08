using DietDB;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class SexController : BaseApiController
    {
        private readonly DietContext _context;

        public SexController(DietContext context)
        {
            _context = context;
        }

        // IMPORTANT: FROM SQL
        [HttpGet]
        [Route("allSexTypesFromView")]
        public async Task<ActionResult<List<Sex>>> GetSexesFromView()
        {
            var result = await _context.SexesDb.FromSqlRaw("SELECT * FROM GetAllSexesFromSqlView").ToListAsync();

            return Ok(result);
        }
    }
}
