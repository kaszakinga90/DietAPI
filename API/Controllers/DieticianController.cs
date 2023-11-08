using Application.Core;
using Application.CQRS.Dieticians;
using Application.DTOs.DieticianDTO;
using Application.Services;
using DietDB;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za zarządzanie dietetykami.
    /// </summary>
    public class DieticianController : BaseApiController
    {
        private readonly ImageService _imageService;
        private readonly DietContext _context;

        public DieticianController(ImageService imageService, DietContext context)
        {
            _imageService = imageService;
            _context = context;
        }

        /// <summary>
        /// Pobiera listę wszystkich dietetyków.
        /// </summary>
        /// <returns>Lista dietetyków.</returns>
        [HttpGet]
        [Route("all")]
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
        public async Task<ActionResult<DieticianGetDTO>> GetDietician(int id)
        {
            return await Mediator.Send(new DieticianDetails.Query { Id = id });
        }

        /// <summary>
        /// Tworzy nowego dietetyka.
        /// </summary>
        /// <param name="Dietician">Dane dietetyka do utworzenia.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDietician(Dietician Dietician)
        {
            await Mediator.Send(new DieticianCreate.Command { Dietician = Dietician });
            return Ok();
        }

        /// <summary>
        /// Aktualizuje dane dietetyka.
        /// </summary>
        /// <param name="id">ID dietetyka.</param>
        /// <param name="dietician">Nowe dane dietetyka.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditDietician(int id, [FromForm] DieticianDTO dieticianDto, [FromForm] IFormFile file)
        {
            var command = new DieticianEdit.Command
            {
                Dietician = dieticianDto,
                File = file
            };
            command.Dietician.Id = id;


            return HandleResult(await Mediator.Send(command));
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

        /// <summary>
        /// Wysyła wiadomość do pacjenta.
        /// </summary>
        /// <param name="message">Wiadomość od dietetyka.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("{dieticianId}/messageToAdmin")]
        public async Task<IActionResult> MessageToAdmin(int dieticianId, MessageToDTO message)
        {
            await Mediator.Send(new MessageToAdminFromDieticianCreate.Command { MessageDTO = message, DieticianId = dieticianId });
            return Ok();
        }

        /// <summary>
        /// Wysyła wiadomość do admina.
        /// </summary>
        /// <param name="message">Wiadomość od dietetyka.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("{dieticianId}/messageToPatient")]
        public async Task<IActionResult> MessageToPatient(int dieticianId, MessageToDTO message)
        {
            await Mediator.Send(new MessageToPatientFromDieticianCreate.Command { MessageDTO = message, DieticianId = dieticianId });
            return Ok();
        }
    }
}
