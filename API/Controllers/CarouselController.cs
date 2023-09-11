using Application.Carousels;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class CarouselController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Carousel>>> GetCarousels()
        {
            return await Mediator.Send(new CarouselList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Carousel>> GetCarousel(int id)
        {
            return await Mediator.Send(new CarouselDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateCarousel(Carousel Carousel)
        {
            await Mediator.Send(new CarouselCreate.Command { Carousel = Carousel });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCarousel(int id, Carousel Carousel)
        {
            Carousel.Id = id;

            await Mediator.Send(new CarouselEdit.Command { Carousel = Carousel });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarousel(int id)
        {
            await Mediator.Send(new CarouselDelete.Command { Id = id });
            return Ok();
        }
    }
}
