﻿using Application.CQRS.DieticiansPatients;
using Application.CQRS.MealSchedules;
using Application.DTOs.MealScheduleDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DieteticianPatientController : BaseApiController
    {
        [HttpGet("{patientId}")]
        public async Task<ActionResult<List<MealScheduleGetDTO>>> GetDieteticianPatient(int patientId)
        {

            var result = await Mediator.Send(new DieticianPatientListDetails.Query { PatientId = patientId });
            return HandleResult(result);
        }
    }
}