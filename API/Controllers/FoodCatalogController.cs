using Application.CQRS.FoodCatalogs;
using Application.DTOs.FoodCatalogDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FoodCatalogController : BaseApiController
    {
        public FoodCatalogController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("getdieticiancatalogs/{dieticianId}")]
        public async Task<IActionResult> GetFoodCatalog(int dieticianId)
        {
            var result = await _mediator.Send(new FoodCatalogDetails.Query { Id = dieticianId });
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

        // DONE : edycja food catalog dietetyka 
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

        // na froncie musi być coś w stylu : ta akcja spowoduje przeniesienie wszystkich produktów do katalogu Wszystkie, czy na pewno?
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

        // DONE : usuwanie foodCatalog (zarówno ogólnego jak i dietetyka) przez admina
        // katalog wspólny - deaktywuje katalog i dania z niego przenosi do katalogu Ogolny (id = 1) (katalog dostępny dla wszystkich/niemodyfikowalny)

        // na froncie musi być coś w stylu : ta akcja spowoduje przeniesienie wszystkich produktów do katalogu Wszystkie, czy na pewno?
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