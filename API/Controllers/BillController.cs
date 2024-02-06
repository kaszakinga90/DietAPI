﻿using Application.CQRS.Bills;
using Application.CQRS.Invitations;
using Application.DTOs.BillDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BillController : BaseApiController
    {
        public BillController(IMediator mediator) : base(mediator)
        {  
        }

        [HttpGet("allBillsForPatient/{patientId}")]
        public async Task<IActionResult> GetPatientsBills(int patientId)
        {
            var result = await _mediator.Send(new BillsPatientList.Query { PatientId = patientId });
            return HandleResult(result);
        }

        [HttpGet("allBillsForDietician/{dieticianId}")]
        public async Task<IActionResult> GetDieticiansBills(int dieticianId)
        {
            var result = await _mediator.Send(new BillsDieticianList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }


        [HttpPost("create/addBill")]
        public async Task<IActionResult> AddBillForDiet(DietSalesBillPostDTO dietSalesBillPostDTO)
        {
            var command = new BillCreate.Command { DietSalesBillPostDTO = dietSalesBillPostDTO };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie wysłano rachunek." });
            }
            return BadRequest(result.Error);
        }

        [HttpGet("details/{billId}")]
        public async Task<IActionResult> GetBillDetails(int billId)
        {
            var result = await _mediator.Send(new BillDetails.Query { DietSalesBillId = billId });
            return HandleResult(result);
        }

        [HttpPut("payTheBill/{dietSalesBillId}")]
        public async Task<IActionResult> PayTheBill(DietSalesBillPutDTO dietSalesBillPutDTO)
        {
            var command = new BillEdit.Command { DietSalesBillPutDTO = dietSalesBillPutDTO };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { message = "Pomyślnie opłacono rachunek." });
            }
            return BadRequest(result.Error);
        }
    }
}

// TODO : przy metodach post, put, dodać walidację