using Application.Core;
using Application.CQRS.Dieticians;
using Application.CQRS.Diplomas;
using Application.CQRS.Logos;
using Application.CQRS.Patients;
using Application.DTOs.DieticianDTO;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.LogoDTO;
using Application.DTOs.MessagesDTO;
using Application.DTOs.PatientDTO;
using Application.FiltersExtensions.DieticianMessages;
using Application.Services;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using static Application.CQRS.Dieticians.MessagesFilters;

namespace API.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za zarządzanie dietetykami.
    /// </summary>
    public class DieticianController : BaseApiController
    {
        private readonly ImageService _imageService;
        private readonly DietContext _context;

        public DieticianController(ImageService imageService, DietContext context, IMediator mediator) : base(mediator)
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
            return await _mediator.Send(new DieticianList.Query());
        }

        /// <summary>
        /// Pobiera szczegóły konkretnego dietetyka na podstawie ID.
        /// </summary>
        /// <param name="id">ID dietetyka.</param>
        /// <returns>Szczegóły dietetyka.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DieticianGetDTO>> GetDietician(int id)
        {
            return await _mediator.Send(new DieticianDetails.Query { Id = id });
        }

        /// <summary>
        /// Tworzy nowego dietetyka.
        /// </summary>
        /// <param name="Dietician">Dane dietetyka do utworzenia.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDietician(Dietician Dietician)
        {
            await _mediator.Send(new DieticianCreate.Command { Dietician = Dietician });
            return Ok();
        }

        /// <summary>
        /// Aktualizuje dane dietetyka.
        /// </summary>
        /// <param name="id">ID dietetyka.</param>
        /// <param name="dietician">Nowe dane dietetyka.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPut("{dieticianId}")]
        public async Task<IActionResult> EditDietician(int dieticianId, [FromForm] DieticianDTO dieticianDto, [FromForm] IFormFile file)
        {
            var command = new DieticianEdit.Command
            {
                Dietician = dieticianDto,
                File = file
            };
            command.Dietician.Id = dieticianId;


            return HandleResult(await _mediator.Send(command));
        }
        [HttpPut("{dieticianId}/editdata")]
        public async Task<IActionResult> EditDieticianData(int dieticianId, DieticianEditDataDTO dietician)
        {
            var command = new DieticianEditData.Command
            {
                DieticianEditData = dietician,
            };
            command.DieticianEditData.Id = dieticianId;

            return HandleResult(await _mediator.Send(command));
        }

        /// <summary>
        /// Pobiera listę wiadomości dla dietetyka o podanym ID.
        /// </summary>
        /// <param name="dieticianId">ID dietetyka.</param>
        /// <returns>Lista wiadomości dla dietetyka.</returns>
        [HttpGet("messages/{dieticianId}")]
        public async Task<ActionResult<PagedList<MessageToDTO>>> GetMessagesForDietician(int dieticianId, [FromQuery] DieticianMessagesParams param)
        {
            var result = await _mediator.Send(new DieticianMessageList.Query { DieticianId = dieticianId, Params = param });

            return HandlePagedResult(result);
        }
        [HttpGet("filters/{dieticianId}")]
        public async Task<ActionResult<DieticianMessagesFiltersDTO>> GetFilters(int dieticianId)
        {
            var result = await _mediator.Send(new FilterList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }

        /// <summary>
        /// Wysyła wiadomość do pacjenta.
        /// </summary>
        /// <param name="message">Wiadomość od dietetyka.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("{dieticianId}/messageToAdmin")]
        public async Task<IActionResult> MessageToAdmin(int dieticianId, MessageToDTO message)
        {
            await _mediator.Send(new MessageToAdminFromDieticianCreate.Command { MessageDTO = message, DieticianId = dieticianId });
            return Ok();
        }

        /// <summary>
        /// Wysyła wiadomość do admina.
        /// </summary>
        /// <param name="message">Wiadomość od dietetyka.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("messageToPatient/{dieticianId}")]
        public async Task<IActionResult> MessageToPatient(int dieticianId, MessageToDTO message)
        {
            await _mediator.Send(new MessageToPatientFromDieticianCreate.Command { MessageDTO = message, DieticianId = dieticianId });
            return Ok();
        }

        [HttpPost("diploma")]
        public async Task<IActionResult> CreateDiploma([FromForm] DiplomaPostDTO diplomaDto, [FromForm] IFormFile file)
        {
            var command = new CreateDiploma.Command
            {
                DiplomaDTO = diplomaDto,
                File = file,
            };

            await _mediator.Send(command);

            return Ok();
        }




    }
}
