using Application.Core;
using Application.CQRS.Diets;
using Application.CQRS.DietsForPatients;
using Application.DTOs.DietDTO;
using Application.FiltersExtensions.Diets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DietController : BaseApiController
    {
        public DietController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{dieticianId}")]
        public async Task<ActionResult<PagedList<DietGetDTO>>> GetDiets(int dieticianId, [FromQuery] DietParams pagingParams)
        {
            var result = await _mediator.Send(new DietList.Query { DieticianId = dieticianId, Params = pagingParams });
            return HandlePagedResult(result);
        }

        [HttpPost("adddiet")]
        public async Task<IActionResult> CreateDiet(DietPostDTO diet)
        {
            await _mediator.Send(new DietCreate.Command { DietPostDTO = diet });
            return Ok();
        }

        [HttpGet("dieticianDiets/{dieticianId}")]
        public async Task<IActionResult> GetDietsForDietician(int dieticianId, [FromQuery] DietParams pagingParams)
        {
            var result = await _mediator.Send(new DietsForDieticianList.Query { DieticianId = dieticianId, Params = pagingParams });
            return HandlePagedResult(result);
        }

        [HttpGet("patientDiets/{patientId}")]
        public async Task<IActionResult> GetDietsForPatient(int patientId, [FromQuery] DietParams pagingParams)
        {
            var result = await _mediator.Send(new DietsForPatientList.Query { PatientId = patientId, Params = pagingParams });
            return HandlePagedResult(result);
        }

        // to będzie w karcie pacjenta - diety dla danego pacjenta od danego dietetyka
        [HttpGet("pc/patientDiets/{patientId}/{dieticianId}")]
        public async Task<IActionResult> GetDietsForPatientFromDietician(int patientId, int dieticianId, [FromQuery] DietParams pagingParams)
        {
            var result = await _mediator.Send(new DietsForPatientFromDieticianList.Query { PatientId = patientId, DieticianId = dieticianId, Params = pagingParams });
            return HandlePagedResult(result);
        }

        [HttpGet("filters/{dieticianId}")]
        public async Task<ActionResult<DietFiltersDTO>> GetFilters(int dieticianId)
        {
            var result = await _mediator.Send(new DietFilters.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }

        // TODO:
        // usuwanie diety

        // TODO :
        // metoda wyślij, argumenty: dieticianId, dietId i? patientId
        // post
        //tabela DietPatient - tabela pośrednia, żeby obsłużyć widoczność diety dla pacjenta
        //

        // get, argumenty patientId, dietId - wyświetli listę wszystkich diet  dla apcjenta (z tabeli DietPatient)
        // w getDTO, DietName, DieticianName, dietId  - generate pdf (szczegóły diety + export do pdf)
    }
}
