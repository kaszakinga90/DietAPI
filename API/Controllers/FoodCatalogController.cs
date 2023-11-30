using Application.CQRS.FoodCatalogs;
using Application.DTOs.FoodCatalogDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FoodCatalogController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodCatalogGetDTO>> GetFoodCatalog(int id)
        {
            return await Mediator.Send(new FoodCatalogDetails.Query { Id = id });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetFoodCatalogs(int dieticianId)
        {
            var result = await Mediator.Send(new FoodCatalogList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }
    }
}
