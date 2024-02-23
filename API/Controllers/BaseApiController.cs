using API.Extensions;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Bazowy kontroler dla wszystkich kontrolerów API.
    /// Zapewnia wspólne zależności i metody pomocnicze.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        // Mediator do obsługi wzorca CQRS.
        protected readonly IMediator _mediator;

        /// <summary>
        /// Konstruktor inicjalizujący mediator.
        /// </summary>
        /// <param name="mediator">Instancja mediatora do obsługi zapytań i komend.</param>
        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Metoda pomocnicza do obsługi wyników zapytań.
        /// </summary>
        /// <typeparam name="T">Typ zwracany przez Result.</typeparam>
        /// <param name="result">Obiekt wynikowy zapytania.</param>
        /// <returns>ActionResult zależny od stanu wyniku.</returns>
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            if (result.IsSucces && result.Value != null)
                return Ok(result.Value);
            if (result.IsSucces && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }

        /// <summary>
        /// Metoda pomocnicza do obsługi stronnicowanych wyników zapytań.
        /// </summary>
        /// <typeparam name="T">Typ zwracany przez Result w PagedList.</typeparam>
        /// <param name="result">Obiekt wynikowy zapytania stronnicowanego.</param>
        /// <returns>ActionResult zależny od stanu wyniku oraz nagłówki paginacji.</returns>
        protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
        {
            if (result == null) return NotFound();
            if (result.IsSucces && result.Value != null)
            {
                Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize, result.Value.TotalCount, result.Value.TotalPages);
                return Ok(result.Value);
            }

            if (result.IsSucces && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
    }
}