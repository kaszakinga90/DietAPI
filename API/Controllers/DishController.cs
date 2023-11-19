using Application.CQRS.Dishes;
using Application.DTOs.DishDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DishController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<DishDTO>>> GetDishes()
        {

            var result = await Mediator.Send(new DishesList.Query());
            return HandleResult(result);
        }
    }
}
