using Application.CQRS.Diets;
using Application.CQRS.Diplomas;
using Application.CQRS.Specializations;
using Application.DTOs.SpecializationDTO;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class SpecializationController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetSpecializations()
        {
            var result = await Mediator.Send(new SpecializationsList.Query());
            return HandleResult(result);
        }

        [HttpGet("{dieticianId}")]
        public async Task<IActionResult> GetDieticianSpecializations(int dieticianId)
        {
            var result = await Mediator.Send(new DieteticianSpecializationList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiet(DieteticianSpecializationPostDTO ds)
        {
            await Mediator.Send(new DieteticianSpecializationCreate.Command { DieteticianSpecializationPostDTOs = ds });
            return Ok();
        }
    }
}
