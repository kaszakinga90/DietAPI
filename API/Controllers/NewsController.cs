using Application.Newses;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class NewsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<News>>> GetNewses()
        {
            return await Mediator.Send(new NewsList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNews(int id)
        {
            return await Mediator.Send(new NewsDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateNews(News News)
        {
            await Mediator.Send(new NewsCreate.Command { News = News });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditNews(int id, News News)
        {
            News.Id = id;

            await Mediator.Send(new NewsEdit.Command { News = News });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            await Mediator.Send(new NewsDelete.Command { Id = id });
            return Ok();
        }
    }
}
