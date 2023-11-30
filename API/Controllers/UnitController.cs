using Application.CQRS.Units;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class UnitController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
            return await Mediator.Send(new UnitDetails.Query { Id = id });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Unit>>> GetUnits()
        {
            return await Mediator.Send(new UnitList.Query());
        }
    }
}
