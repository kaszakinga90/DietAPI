using Application.CQRS.Units;
using Application.DTOs.UnitDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UnitController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitGetDTO>> GetUnit(int id)
        {
            return await Mediator.Send(new UnitDetails.Query { Id = id });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UnitGetDTO>>> GetUnits()
        {
            var result = await Mediator.Send(new UnitList.Query());
            return HandleResult(result);
        }
    }
}
