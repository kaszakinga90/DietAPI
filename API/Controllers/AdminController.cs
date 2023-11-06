using Application.Core;
using Application.CQRS.Admins;
using Application.DTOs.AdminDTO;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za zarządzanie adminami.
    /// </summary>
    public class AdminController : BaseApiController
    {
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
        public async Task<ActionResult<AdminDTO>> GetAdmin(int id)
        {
            return await Mediator.Send(new AdminDetails.Query { Id = id });
        }

        /// <summary>
        /// Tworzy nowego admina.
        /// </summary>
        /// <param name="Admin">Dane admina do utworzenia.</param>
        /// <returns>StatusesDb operacji.</returns>
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
        /// <returns>StatusesDb operacji.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdmin(int id, AdminDTO admin)
        {
            admin.Id = id;

            return HandleResult(await Mediator.Send(new AdminEdit.Command { Admin = admin }));
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
        /// <returns>StatusesDb operacji.</returns>
        [HttpPost("message")]
        public async Task<IActionResult> MessageToDietetician(MessageToDTO message)
        {
            await Mediator.Send(new MessageToDieteticianFromAdminCreate.Command { MessageDTO = message });
            return Ok();
        }

        /// <summary>
        /// Wysyła wiadomość do pacjenta.
        /// </summary>
        /// <param name="message">Wiadomość dla pacjenta.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPost("message")]
        public async Task<IActionResult> MessageToPatient(MessageToDTO message)
        {
            await Mediator.Send(new MessageToPatientFromAdminCreate.Command { MessageDTO = message });
            return Ok();
        }
    }
}
