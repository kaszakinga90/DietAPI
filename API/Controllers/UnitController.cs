using Application.CQRS.Measures;
using Application.CQRS.Units;
using Application.DTOs.MeasureDTO;
using Application.DTOs.UnitDTO;
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
        public async Task<ActionResult<List<UnitGetDTO>>> GetUnits()
        {
            var result = await Mediator.Send(new UnitList.Query());
            return HandleResult(result);
        }
    }
}
