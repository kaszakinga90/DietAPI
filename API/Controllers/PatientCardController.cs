using Application.CQRS.PatientCards;
using Application.CQRS.PatientCards.Surveys;
using Application.CQRS.PatientCards.TestsResults;
using Application.CQRS.Patients;
using Application.DTOs.PatientCardDTO;
using Application.DTOs.Surveys;
using Application.DTOs.TestsResultsDTO;
using Application.FiltersExtensions.PatientsCards;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PatientCardController : BaseApiController
    {
        public PatientCardController(IMediator mediator) : base(mediator)
        {
        }

        // IMPORTANT : FROM SQL
        [HttpGet("{dieticianId}/{patientId}")]
        public async Task<IActionResult> GetPatientCardSP(int patientId, int dieticianId)
        {
            var result = await _mediator.Send(new PatientCardDetails.Query { PatientId = patientId, DieticianId = dieticianId });
            return HandleResult(result);
        }

        // IMPORTANT : FROM SQL - tworzenie obiektu PatientCard za pomocą stored procedures
        [HttpPost("create/")]
        public async Task<IActionResult> CreatePatientCardSP(PatientCardPostDTO pc)
        {
            var command = new PatientCardCreate.Command { PatientCardPostDTO = pc };
            var result = await _mediator.Send(command);

            if (result.IsSucces)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpGet("list/{dieticianId}")]
        public async Task<IActionResult> GetAllPatientsCards(int dieticianId, [FromQuery] PatientCardParams param)
        {
            var result = await _mediator.Send(new PatientCardList.Query {
                DieticianId = dieticianId, 
                Params = param 
            });
            return HandlePagedResult(result);
        }

        // DONE   - dodane filtrowane i sortowanie
        [HttpGet("filters")]
        public async Task<IActionResult> GetFiltersPatientCardForDietician()
        {
            var result = await _mediator.Send(new PatientsFilterList.Query());
            return HandleResult(result);
        }

        [HttpGet("listforpatient/{patientId}")]
        public async Task<IActionResult> GetAllForPatientsCards(int patientId, [FromQuery] PatientCardParams param)
        {
            var result = await _mediator.Send(new PatientCardForPatientList.Query { 
                PatientId = patientId, 
                Params = param 
            });
            return HandlePagedResult(result);
        }

        [HttpGet("getallpatientsurveys/{dieticianId}/{patientCardId}")]
        public async Task<IActionResult> GetAllPatientSurveys(int dieticianId,int patientCardId)
        {
            var result = await _mediator.Send(new PatientCardSurveysList.Query { 
                DieteticianId = dieticianId, 
                PatientCardId=patientCardId 
            });
            return HandleResult(result);
        }

        [HttpGet("getallpatientsurveysforpatient/{patientCardId}")]
        public async Task<IActionResult> GetAllPatientSurveysForPatient(int patientCardId)
        {
            var result = await _mediator.Send(new PatientCardSurveysForPatientList.Query {  PatientCardId = patientCardId });
            return HandleResult(result);
        }

        [HttpGet("getallpatienttestresults/{dieticianId}/{patientCardId}")]
        public async Task<IActionResult> GetAllPatientTestResults(int dieticianId,int patientCardId)
        {
            var result = await _mediator.Send(new PatientCardTestsResultList.Query { 
                DieteticianId = dieticianId, 
                PatientCardId=patientCardId 
            });
            return HandleResult(result);
        } 

        [HttpGet("getallpatienttestresultsforpatient/{patientCardId}")]
        public async Task<IActionResult> GetAllPatientTestResultsForPatient(int patientCardId)
        {
            var result = await _mediator.Send(new PatientCardTestsResultForPatientList.Query { PatientCardId = patientCardId });
            return HandleResult(result);
        }

        [HttpPost("createsurvey")]
        public async Task<IActionResult> CreateSurvey(SurveyPostDTO surveyPostDTO)
        {
            var command = new SurveyCreate.Command { surveyPostDTO = surveyPostDTO };
            var result = await _mediator.Send(command);

            if (result.IsSucces)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        } 
        
        [HttpPost("createtestresult")]
        public async Task<IActionResult> CreateTestResult(TestResultPostDTO testResultPostDTO)
        {
            var command = new TestResultCreate.Command { TestResultPostDTO = testResultPostDTO };
            var result = await _mediator.Send(command);

            if (result.IsSucces)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpGet("getallpatienttestresultsforpatientNopagination/{patientId}")]
        public async Task<IActionResult> GetAllPatientTestResultsForPatientNoPagination(int patientId)
        {
            var result = await _mediator.Send(new PatientCardNoPaginationList.Query { PatientId = patientId });
            return HandleResult(result);
        }

        // DONE : metoda do usuwania test result
        [HttpDelete("testResult/delete/{testResultId}")]
        public async Task<IActionResult> DeleteTestResult(int testResultId)
        {
            var command = new TestResultDelete.Command { TestResultId = testResultId };

            return HandleResult(await _mediator.Send(command));
        }

        // DONE : metoda do usuwania survey
        [HttpDelete("survey/delete/{surveyId}")]
        public async Task<IActionResult> DeleteSurvey(int surveyId)
        {
            var command = new SurveyDelete.Command { SurveyId = surveyId };

            return HandleResult(await _mediator.Send(command));
        }
    }
}