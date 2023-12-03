using Application.CQRS.FoodCatalogs;
using Application.DTOs.FoodCatalogDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FoodCatalogController : BaseApiController
    {
        //pobiera katalog po id
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodCatalogGetDTO>> GetFoodCatalog(int id)
        {
            return await Mediator.Send(new FoodCatalogDetails.Query { Id = id });
        }

        //pobiera katalogi dań dostępne dla dietetyka (czyli z bazy wspólnej, gdzie DieticianID == NULL oraz te utworzone przez tego dietetyka)
        [HttpGet("all")]
        public async Task<IActionResult> GetFoodCatalogs(int dieticianId)
        {
            var result = await Mediator.Send(new FoodCatalogList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFoodCatalog([FromForm] FoodCatalogPostDTO FoodCatalogPostDTO)
        {
            await Mediator.Send(new FoodCatalogCreate.Command { FoodCatalogPostDTO = FoodCatalogPostDTO });
            return Ok();
        }
    }
}
