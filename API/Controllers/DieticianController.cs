using Application.Core;
using Application.CQRS.Dieticians;
using Application.DTOs.DieticianDTO;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za zarządzanie dietetykami.
    /// </summary>
    public class DieticianController : BaseApiController
    {
        /// <summary>
        /// Pobiera listę wszystkich dietetyków.
        /// </summary>
        /// <returns>Lista dietetyków.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Dietician>>> GetDieticians()
        {
            return await Mediator.Send(new DieticianList.Query());
        }

        /// <summary>
        /// Pobiera szczegóły konkretnego dietetyka na podstawie ID.
        /// </summary>
        /// <param name="id">ID dietetyka.</param>
        /// <returns>Szczegóły dietetyka.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DieticianDTO>> GetDietician(int id)
        {
            return await Mediator.Send(new DieticianDetails.Query { Id = id });
        }

        /// <summary>
        /// Tworzy nowego dietetyka.
        /// </summary>
        /// <param name="Patient">Dane dietetyka do utworzenia.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDietician(Dietician Dietician)
        {
            await Mediator.Send(new DieticianCreate.Command { Dietician = Dietician });
            return Ok();
        }

        /// <summary>
        /// Wysyła wiadomość do pacjenta.
        /// </summary>
        /// <param name="message">Wiadomość od dietetyka.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPost("message")]
        public async Task<IActionResult> MessageToPatient(MessageToDTO message)
        {
            await Mediator.Send(new MessageToPatientFromDieticianCreate.Command { MessageDTO = message });
            return Ok();
        }

        /// <summary>
        /// Wysyła wiadomość do admina.
        /// </summary>
        /// <param name="message">Wiadomość od dietetyka.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPost("message")]
        public async Task<IActionResult> MessageToAdmin(MessageToDTO message)
        {
            await Mediator.Send(new MessageToAdminFromDieticianCreate.Command { MessageDTO = message });
            return Ok();
        }

        /// <summary>
        /// Aktualizuje dane dietetyka.
        /// </summary>
        /// <param name="id">ID dietetyka.</param>
        /// <param name="dietician">Nowe dane dietetyka.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditDietician(int id, DieticianDTO dietician)
        {
            dietician.Id = id;

            return HandleResult(await Mediator.Send(new DieticianEdit.Command { Dietician = dietician }));
        }

        /// <summary>
        /// Pobiera listę wiadomości dla dietetyka o podanym ID.
        /// </summary>
        /// <param name="dieticianId">ID dietetyka.</param>
        /// <returns>Lista wiadomości dla dietetyka.</returns>
        [HttpGet("{dieticianId}/messages")]
        public async Task<ActionResult<PagedList<MessageToDTO>>> GetMessagesForDietician(int dieticianId, [FromQuery] PagingParams param)
        {
            var result = await Mediator.Send(new DieticianMessageList.Query { DieticianId = dieticianId, Params = param });

            return HandlePagedResult(result);
        }
    }
}
