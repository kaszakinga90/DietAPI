using Application.Core;
using Application.CQRS.Admins;
using Application.DTOs.AdminDTO;
using Application.Services;
using DietDB;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za zarządzanie adminami.
    /// </summary>
    public class AdminController : BaseApiController
    {
        private readonly ImageService _imageService;
        private readonly DietContext _context;

        public AdminController(ImageService imageService, DietContext context)
        {
            _imageService = imageService;
            _context = context;
        }

        /// <summary>
        /// Pobiera listę wszystkich adminów.
        /// </summary>
        /// <returns>Lista adminów.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Admin>>> GetAdmins()
        {
            return await Mediator.Send(new AdminList.Query());
        }

        /// <summary>
        /// Pobiera szczegóły konkretnego admina na podstawie ID.
        /// </summary>
        /// <param name="id">ID admina.</param>
        /// <returns>Szczegóły admina.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminGetDTO>> GetAdmin(int id)
        {
            return await Mediator.Send(new AdminDetails.Query { Id = id });
        }

        /// <summary>
        /// Tworzy nowego admina.
        /// </summary>
        /// <param name="Admin">Dane admina do utworzenia.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAdmin(Admin Admin)
        {
            await Mediator.Send(new AdminCreate.Command { Admin = Admin });
            return Ok();
        }

        /// <summary>
        /// Aktualizuje dane admina.
        /// </summary>
        /// <param name="id">ID admina.</param>
        /// <param name="admin">Nowe dane admina.</param>
        /// <returns>Status operacji.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdmin(int id, [FromForm] AdminDTO adminDTO, [FromForm] IFormFile file)
        {
            var command = new AdminEdit.Command
            {
                Admin = adminDTO,
                File = file
            };
            command.Admin.Id = id;

            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>
        /// Pobiera listę wiadomości dla admina o podanym ID.
        /// </summary>
        /// <param name="adminId">ID admina.</param>
        /// <returns>Lista wiadomości dla admina.</returns>
        [HttpGet("{adminId}/messages")]
        public async Task<ActionResult<PagedList<MessageToDTO>>> GetMessagesForAdmin(int adminId, [FromQuery] PagingParams param)
        {
            var result = await Mediator.Send(new AdminMessageList.Query { AdminId = adminId, Params = param });

            return HandlePagedResult(result);
        }

        /// <summary>
        /// Wysyła wiadomość do dietetyka.
        /// </summary>
        /// <param name="message">Wiadomość dla dietetyka.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("{adminId}/messageToDietician")]
        public async Task<IActionResult> MessageToDietetician(int adminId, MessageToDTO message)
        {
            await Mediator.Send(new MessageToDieteticianFromAdminCreate.Command { MessageDTO = message, AdminId = adminId });
            return Ok();
        }

        /// <summary>
        /// Wysyła wiadomość do pacjenta.
        /// </summary>
        /// <param name="message">Wiadomość dla pacjenta.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("{adminId}/messageToPatient")]
        public async Task<IActionResult> MessageToPatient(int adminId, MessageToDTO message)
        {
            await Mediator.Send(new MessageToPatientFromAdminCreate.Command { MessageDTO = message, AdminId = adminId });
            return Ok();
        }
    }
}
