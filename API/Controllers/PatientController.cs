using Application.Core;
using Application.CQRS.Patients;
using Application.DTOs.MessagesDTO;
using Application.DTOs.PatientDTO;
using Application.FiltersExtensions.PatientMessages;
using Application.Services;
using DietDB;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using static Application.CQRS.Patients.MessagesFilters;

namespace API.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za zarządzanie pacjentami.
    /// </summary>
    public class PatientController : BaseApiController
    {
        private readonly ImageService _imageService;
        private readonly DietContext _context;

        public PatientController(ImageService imageService, DietContext context)
        {
            _imageService = imageService;
            _context = context;
        }

        /// <summary>
        /// Pobiera listę wszystkich pacjentów.
        /// </summary>
        /// <returns>Lista pacjentów.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            return await Mediator.Send(new PatientList.Query());
        }

        /// <summary>
        /// Pobiera szczegóły konkretnego pacjenta na podstawie ID.
        /// </summary>
        /// <param name="id">ID pacjenta.</param>
        /// <returns>Szczegóły pacjenta.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientGetDTO>> GetPatient(int id)
        {
            return await Mediator.Send(new PatientDetails.Query { Id = id });
        }

        /// <summary>
        /// Tworzy nowego pacjenta.
        /// </summary>
        /// <param name="Patient">Dane pacjenta do utworzenia.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patient Patient)
        {
            await Mediator.Send(new PatientCreate.Command { Patient = Patient });
            return Ok();
        }

        /// <summary>
        /// Aktualizuje dane pacjenta.
        /// </summary>
        /// <param name="id">ID pacjenta.</param>
        /// <param name="patient">Nowe dane pacjenta.</param>
        /// <returns>StatusesDb operacji.</returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> EditPatient(int id, [FromForm] PatientDTO patient)
        //{
        //    patient.Id = id;
        //    var patientUpdate = await _context.PatientsDb.FindAsync(patient.Id);

        //    if (patient.File != null)
        //    {
        //        var imageResult = await _imageService.AddImageAsync(patient.File);

        //        if (imageResult.Error != null)
        //            return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

        //        if (!string.IsNullOrEmpty(patientUpdate.PublicId)) await _imageService.DeleteImageAsync(patientUpdate.PublicId);

        //        patientUpdate.PictureUrl = imageResult.SecureUrl.ToString();
        //        patientUpdate.PublicId = imageResult.PublicId;
        //    }

        //    return HandleResult(await Mediator.Send(new PatientEdit.Command { Patient = patient }));
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPatient(int id, [FromForm] PatientDTO patientDTO, [FromForm] IFormFile file)
        {
            var command = new PatientEdit.Command
            {
                Patient = patientDTO,
                File = file
            };
            command.Patient.Id = id;

            return HandleResult(await Mediator.Send(command));
        }
        [HttpPut("{id}/editdata")]
        public async Task<IActionResult> EditPatientData(int id, PatientEditDataDTO patient)
        {
            var command = new PatientEditData.Command
            {
                PatientEditData = patient,
            };
            command.PatientEditData.Id = id;

            return HandleResult(await Mediator.Send(command));
        }

        [HttpGet("{patientId}/messages")]
        public async Task<ActionResult<PagedList<MessageToDTO>>> GetMessagesForPatient(int patientId, [FromQuery] PatientMessagesParams param)
        {
            var result = await Mediator.Send(new PatientMessageList.Query { PatientId = patientId, Params = param });

            return HandlePagedResult(result);
        }
        [HttpGet("filters/{patientId}")]
        public async Task<ActionResult<MessagesFiltersDTO>> GetFilters(int patientId)
        {
            var result = await Mediator.Send(new FilterList.Query { PatientId=patientId});
            return HandleResult(result);
        }

        /// <summary>
        /// Wysyła wiadomość do dietetyka.
        /// </summary>
        /// <param name="message">Wiadomość dla dietetyka.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("messageToDietician/{patientId}")]
        public async Task<IActionResult> MessageToDietetician(int patientId, MessageToDTO message)
        {
            await Mediator.Send(new MessageToDieteticianFromPatientCreate.Command { MessageDTO = message, PatientId = patientId });
            return Ok();
        }

        /// <summary>
        /// Wysyła wiadomość do admina.
        /// </summary>
        /// <param name="message">Wiadomość dla admina.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("{patientId}/messageToAdmin")]
        public async Task<IActionResult> MessageToAdmin(int patientId, MessageToDTO message)
        {
            await Mediator.Send(new MessageToAdminFromPatientCreate.Command { MessageDTO = message, PatientId = patientId });
            return Ok();
        }

    }
}
