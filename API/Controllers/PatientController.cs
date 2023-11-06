using Application.Core;
using Application.CQRS.Patients;
using Application.DTOs.PatientDTO;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za zarządzanie pacjentami.
    /// </summary>
    public class PatientController : BaseApiController
    {
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
        public async Task<ActionResult<PatientDTO>> GetPatient(int id)
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
        /// Wysyła wiadomość do dietetyka.
        /// </summary>
        /// <param name="message">Wiadomość dla dietetyka.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPost("message")]
        public async Task<IActionResult> MessageToDietetician(MessageToDTO message)
        {
            await Mediator.Send(new MessageToDieteticianFromPatientCreate.Command { MessageDTO = message });
            return Ok();
        }

        /// <summary>
        /// Aktualizuje dane pacjenta.
        /// </summary>
        /// <param name="id">ID pacjenta.</param>
        /// <param name="patient">Nowe dane pacjenta.</param>
        /// <returns>StatusesDb operacji.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPatient(int id, PatientDTO patient)
        {
            patient.Id = id;

            return HandleResult(await Mediator.Send(new PatientEdit.Command { Patient = patient }));
        }

        /// <summary>
        /// Pobiera listę wiadomości dla pacjenta o podanym ID.
        /// </summary>
        /// <param name="patientId">ID pacjenta.</param>
        /// <returns>Lista wiadomości dla pacjenta.</returns>
        [HttpGet("{patientId}/messages")]
        public async Task<ActionResult<PagedList<MessageToDTO>>> GetMessagesForPatient(int patientId, [FromQuery]PagingParams param)
        {
            var result = await Mediator.Send(new PatientMessageList.Query { PatientId = patientId, Params = param });

           return HandlePagedResult(result);
        }

    }
}