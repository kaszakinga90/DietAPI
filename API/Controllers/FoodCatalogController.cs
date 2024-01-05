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

        //pobiera katalog po id
        [HttpGet("getdieticiancatalogs/{dieticianId}")]
        public async Task<ActionResult<FoodCatalogGetDTO>> GetFoodCatalog(int dieticianId)
        {
            return await _mediator.Send(new FoodCatalogDetails.Query { Id = dieticianId });
        }

        //pobiera katalogi dań dostępne dla dietetyka (czyli z bazy wspólnej, gdzie DieticianID == NULL oraz te utworzone przez tego dietetyka)
        [HttpGet("getallcatalogs/{dieticianId}")]
        public async Task<IActionResult> GetFoodCatalogs(int dieticianId)
        {
            var result = await _mediator.Send(new FoodCatalogList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFoodCatalog(FoodCatalogPostDTO FoodCatalogPostDTO)
        {
            var result = await _mediator.Send(new FoodCatalogCreate.Command { FoodCatalogPostDTO = FoodCatalogPostDTO });
            return Ok(result.Value); // Zwraca obiekt DTO z ID
        }
    }
}
