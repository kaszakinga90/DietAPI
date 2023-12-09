using Microsoft.AspNetCore.Mvc;
using Application.DTOs.SexDTO;
using Application.CQRS.Sexes;

namespace API.Controllers
{
    public class SexController : BaseApiController
    {
        // IMPORTANT : FROM SQL
        [HttpGet]
        [Route("allSexTypesFromView")]
        public async Task<ActionResult<List<SexGetDTO>>> GetSexesFromView()
        {
            var result = await Mediator.Send(new SexesList.Query());
            return HandleResult(result);
        }
    }
}
