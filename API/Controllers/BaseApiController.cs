using API.Extensions;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Kontroler podstawowy, z którego dziedziczą wszystkie inne kontrolery
    /// Zapewnia wspólną obsługę metod
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        // Mediator do obsługi wzorca CQRS
        protected readonly IMediator _mediator;
         
        /// <summary>
        /// Inicjalizacja mediatora
        /// </summary>
        /// <param name="mediator">Instancja mediatora słuząca do obsługi zapytań i komend</param>
        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Metoda pomocnicza do obsługi wyników zapytań
        /// </summary>
        /// <typeparam name="T">Typ zwracany przez Result</typeparam>
        /// <param name="result">Obiekt wynikowy zapytania</param>
        /// <returns>ActionResult, który zalezy od stanu wyniku</returns>
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
        /// Metoda pomocnicza do obsługi wyników zapytań
        /// </summary>
        /// <typeparam name="T">Typ zwracany przez Result w PagedList</typeparam>
        /// <param name="result">Obiekt wynikowy zapytania</param>
        /// <returns>ActionResult, który zalezy od stanu wyniku wraz z nagłówkami paginacji</returns>
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