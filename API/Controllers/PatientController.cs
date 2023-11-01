using Application.Core;
using Application.CQRS.PatientDTOs;
using Application.CQRS.Patients;
using Application.DTOs.MessagesDTO;
using Application.Services;
using DietDB;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

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
        /// <returns>Status operacji.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patient Patient)
        {
            return HandleResult(await Mediator.Send(new PatientCreate.Command { Patient = Patient }));
            
        }

        /// <summary>
        /// Wysyła wiadomość do dietetyka.
        /// </summary>
        /// <param name="message">Wiadomość dla dietetyka.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("message")]
        public async Task<IActionResult> MessageToDietetician(MessageToDieteticianDTO message)
        {
            await Mediator.Send(new MessageToDieteticianCreate.Command { MessageDTO = message });
            return Ok();
        }

        /// <summary>
        /// Aktualizuje dane pacjenta.
        /// </summary>
        /// <param name="id">ID pacjenta.</param>
        /// <param name="patient">Nowe dane pacjenta.</param>
        /// <returns>Status operacji.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPatient(int id, [FromForm] PatientDTO patient)
        {
            patient.Id = id;
            var patientUpdate=await _context.Patients.FindAsync(patient.Id);

            if(patient.File != null)
            {
                var imageResult=await _imageService.AddImageAsync(patient.File);

                if(imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                if(!string.IsNullOrEmpty(patientUpdate.PublicId)) await _imageService.DeleteImageAsync(patientUpdate.PublicId);

                patientUpdate.PictureUrl=imageResult.SecureUrl.ToString();
                patientUpdate.PublicId=imageResult.PublicId;
            }

            return HandleResult(await Mediator.Send(new PatientEdit.Command { Patient = patient }));
        }

        /// <summary>
        /// Pobiera listę wiadomości dla pacjenta o podanym ID.
        /// </summary>
        /// <param name="patientId">ID pacjenta.</param>
        /// <returns>Lista wiadomości dla pacjenta.</returns>
        [HttpGet("{patientId}/messages")]
        public async Task<ActionResult<PagedList<MessageToPatientDTO>>> GetMessagesForPatient(int patientId, [FromQuery]PagingParams param)
        {
            var result = await Mediator.Send(new PatientMessageList.Query { PatientId = patientId, Params = param });

           return HandlePagedResult(result);
        }

    }
}
