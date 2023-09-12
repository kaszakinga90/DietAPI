using Application.Articles;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class ArticleController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Article>>> GetArticles()
        {
            return await Mediator.Send(new ArticleList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            return await Mediator.Send(new ArticleDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateArticle(Article Article)
        {
            await Mediator.Send(new ArticleCreate.Command { Article = Article });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditArticle(int id, Article Article)
        {
            Article.Id = id;

            await Mediator.Send(new ArticleEdit.Command { Article = Article });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            await Mediator.Send(new ArticleDelete.Command { Id = id });
            return Ok();
        }
    }
}
