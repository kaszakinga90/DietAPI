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
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodCatalogGetDTO>> GetFoodCatalog(int id)
        {
            return await _mediator.Send(new FoodCatalogDetails.Query { Id = id });
        }

        //pobiera katalogi dań dostępne dla dietetyka (czyli z bazy wspólnej, gdzie DieticianID == NULL oraz te utworzone przez tego dietetyka)
        [HttpGet("all")]
        public async Task<IActionResult> GetFoodCatalogs(int dieticianId)
        {
            var result = await _mediator.Send(new FoodCatalogList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFoodCatalog([FromForm] FoodCatalogPostDTO FoodCatalogPostDTO)
        {
            await _mediator.Send(new FoodCatalogCreate.Command { FoodCatalogPostDTO = FoodCatalogPostDTO });
            return Ok();
        }
    }
}
