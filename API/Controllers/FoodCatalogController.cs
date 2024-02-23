using Application.CQRS.FoodCatalogs;
using Application.DTOs.FoodCatalogDTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FoodCatalogController : BaseApiController
    {
        public FoodCatalogController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("getcatalogdetails/{foodCatalogId}")]
        public async Task<IActionResult> GetFoodCatalogDetails(int foodCatalogId)
        {
            var result = await _mediator.Send(new FoodCatalogDetails.Query { Id = foodCatalogId });
            return HandleResult(result);
        }

        //pobiera katalogi dań dostępne dla dietetyka (czyli z bazy wspólnej, gdzie DieticianID == NULL oraz te utworzone przez tego dietetyka)
        [HttpGet("getallcatalogs/{dieticianId}")]
        public async Task<IActionResult> GetFoodCatalogs(int dieticianId)
        {
            var result = await _mediator.Send(new FoodCatalogList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }
        
        //pobiera prywatne katalogi dań dostępne dla dietetyka 
        [HttpGet("getalldieticiancatalogs/{dieticianId}")]
        public async Task<IActionResult> GetFoodDieticianCatalogs(int dieticianId)
        {
            var result = await _mediator.Send(new FoodCatalogDieticianList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateFoodCatalog(FoodCatalogPostDTO FoodCatalogPostDTO)
        {
            var command = new FoodCatalogCreate.Command { FoodCatalogPostDTO = FoodCatalogPostDTO };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie utworzono katalog." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPut("edit/{foodCatalogId}")]
        public async Task<IActionResult> EditDieticianFoodCatalog(int foodCatalogId, FoodCatalogDieticianEditDTO foodCatalogDTO)
        {
            var command = new FoodCatalogDieticianEdit.Command
            {
                FoodCatalogDieticianEditDTO = foodCatalogDTO,
            };
            command.FoodCatalogDieticianEditDTO.Id = foodCatalogId;

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano katalog." });
            }
            return BadRequest(result.Error);
        }

        // katalog dietetyka - deaktywuje jego katalog i dania z niego przenosi do katalogu Wszystkie (dedykowany katalog dla dietetyka tworzony wraz z rejestracją)
        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpDelete("delete/{foodCatalogId}")]
        public async Task<IActionResult> DeleteDieticianFoodCatalog(int foodCatalogId)
        {
            var command = new FoodCatalogByDieticianDelete.Command { FoodCatalogId = foodCatalogId };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie usunięto katalog." });
            }
            return BadRequest(result.Error);
        }

        // katalog wspólny - deaktywuje katalog i dania z niego przenosi do katalogu Ogolny (id = 1) (katalog dostępny dla wszystkich/niemodyfikowalny)
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpDelete("deleteByAdmin/{foodCatalogId}")]
        public async Task<IActionResult> DeleteFoodCatalog(int foodCatalogId)
        {
            var command = new FoodCatalogDelete.Command { FoodCatalogId = foodCatalogId };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie usunięto katalog." });
            }
            return BadRequest(result.Error);
        }
    }
}