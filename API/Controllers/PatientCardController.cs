using Application.Core;
using Application.CQRS.PatientCards;
using Application.DTOs.PatientCardDTO;
using Application.DTOs.Surveys;
using Application.DTOs.TestsResultsDTO;
using Application.FiltersExtensions.PatientsCards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    public class PatientCardController : BaseApiController
    {
        public PatientCardController(IMediator mediator) : base(mediator)
        {
        }

        // IMPORTANT : FROM SQL
        [HttpGet("{dieticianId}/{patientId}")]
        public async Task<ActionResult<PatientCard>> GetPatientCardSP(int patientId, int dieticianId)
        {
            await _mediator.Send(new PatientCardDetails.Query { PatientId = patientId, DieticianId = dieticianId });
            return Ok();
        }

        // IMPORTANT : FROM SQL - tworzenie obiektu PatientCard za pomocą stored procedures
        [HttpPost("create/")]
        public async Task<ActionResult<PatientCard>> CreatePatientCardSP(PatientCardPostDTO pc)
        {
            await _mediator.Send(new PatientCardCreate.Command { PatientCardPostDTO = pc });
            return Ok();
        }
        [HttpGet("list/{dieticianId}")]
        public async Task<ActionResult<PagedList<PatientCardGetDTO>>> GetAllPatientsCards(int dieticianId, [FromQuery] PatientCardParams param)
        {
            var result = await _mediator.Send(new PatientCardList.Query { DieticianId = dieticianId, Params = param });

            return HandlePagedResult(result);
        }
        [HttpGet("listforpatient/{patientId}")]
        public async Task<ActionResult<PagedList<PatientCardGetDTO>>> GetAllForPatientsCards(int patientId, [FromQuery] PatientCardParams param)
        {
            var result = await _mediator.Send(new PatientCardForPatientList.Query { PatientId = patientId, Params = param });

            return HandlePagedResult(result);
        }
        [HttpGet("getallpatientsurveys/{dieticianId}/{patientCardId}")]
        public async Task<IActionResult> GetAllPatientSurveys(int dieticianId,int patientCardId)
        {
            var result = await _mediator.Send(new PatientCardSurveysList.Query { DieteticianId = dieticianId, PatientCardId=patientCardId });
            return HandleResult(result);
        }
        [HttpGet("getallpatientsurveysforpatient/{patientCardId}")]
        public async Task<IActionResult> GetAllPatientSurveysForPatient(int patientCardId)
        {
            var result = await _mediator.Send(new PatientCardSurveysForPatientList.Query {  PatientCardId=patientCardId });
            return HandleResult(result);
        }
        [HttpGet("getallpatienttestresults/{dieticianId}/{patientCardId}")]
        public async Task<IActionResult> GetAllPatientTestResults(int dieticianId,int patientCardId)
        {
            var result = await _mediator.Send(new PatientCardTestsResultList.Query { DieteticianId = dieticianId, PatientCardId=patientCardId });
            return HandleResult(result);
        } 
        [HttpGet("getallpatienttestresultsforpatient/{patientCardId}")]
        public async Task<IActionResult> GetAllPatientTestResultsForPatient(int patientCardId)
        {
            var result = await _mediator.Send(new PatientCardTestsResultForPatientList.Query { PatientCardId=patientCardId });
            return HandleResult(result);
        }
        [HttpPost("createsurvey")]
        public async Task<ActionResult<SurveyPostDTO>> CreateSurvey(SurveyPostDTO surveyPostDTO)
        {
            var result = await _mediator.Send(new SurveyCreate.Command { surveyPostDTO = surveyPostDTO });
            if (result.IsSucces) return Ok(result.Value);
            return BadRequest(result.Error);
        } [HttpPost("createtestresult")]
        public async Task<ActionResult<TestResultPostDTO>> CreateTestResult(TestResultPostDTO testResultPostDTO)
        {
            var result = await _mediator.Send(new TestResultCreate.Command { testResultPost = testResultPostDTO });
            if (result.IsSucces) return Ok(result.Value);
            return BadRequest(result.Error);
        }
        //[HttpGet("getallpatienttestresultsforpatientNopagination/{patientId}")]
        //public async Task<IActionResult> GetAllPatientTestResultsForPatientNoPagination(int patientId)
        //{
        //    var result = await _mediator.Send(new PatientCardTestsResultForPatientList.Query { PatientCardId = patientId });
        //    return HandleResult(result);
        //}
            [HttpGet("getallpatienttestresultsforpatientNopagination/{patientId}")]
        public async Task<IActionResult> GetAllPatientTestResultsForPatientNoPagination(int patientId)
        {
            var result = await _mediator.Send(new PatientCardNoPaginationList.Query { PatientId = patientId });
            return HandleResult(result);
        }
        // TODO : lista wszystkich kart pacjentów u danego dietetyka + filtry i paginacja (u dietetyka, filtr po nazwie pacjenta)
        // edycja karty pacjenta
    }
}



// TODO - delete do uzupełnienia do survey i testresult