using Application.CQRS.Diets;
using Application.CQRS.DietsForPatients;
using Application.CQRS.DietsForPatients.DietPatients;
using Application.DTOs.DietDTO;
using Application.DTOs.DietPatientDTO;
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
        public async Task<IActionResult> GetDiets(int dieticianId, [FromQuery] DietParams pagingParams)
        {
            var result = await _mediator.Send(new DietList.Query { DieticianId = dieticianId, Params = pagingParams });
            return HandlePagedResult(result);
        }

        [HttpPost("adddiet")]
        public async Task<IActionResult> CreateDiet(DietPostDTO diet)
        {
            var result = await _mediator.Send(new DietCreate.Command { DietPostDTO = diet });
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Dieta została dodana pomyślnie." });
            }
            return BadRequest(result.Error);
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

        // DONE : metoda do wysłania diety do pacjenta
        [HttpPost("publishDiet")]
        public async Task<IActionResult> PublishDietToPatient(DietPatientPostDTO dietPatientPostDTO)
        {
            var result = await _mediator.Send(new DietPatientCreate.Command { DietPatientPostDTO = dietPatientPostDTO });
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Dieta została wysłana." });
            }
            return BadRequest(result.Error);
        }

        // DONE : metoda do usuwania diety (tylko, gdy nie została wysłana)
        [HttpDelete("deleteDiet/{dietId}")]
        public async Task<IActionResult> DeleteDiet(int dietId)
        {
            var command = new DietDelete.Command { DietId = dietId };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Dieta została usunięta." });
            }
            return BadRequest(result.Error);
        }

        // DONE : szczegóły diety - na widok i do raportu
        [HttpGet("details/{dietId}")]
        public async Task<IActionResult> GetDietDetails(int dietId)
        {
            var query = new DietDetails.Query { DietId = dietId };
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        // TODO : get, argumenty patientId, dietId - wyświetli listę wszystkich diet  dla apcjenta (z tabeli DietPatient)
    }
}
