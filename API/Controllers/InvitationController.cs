﻿using Application.CQRS.Invitations;
using Application.DTOs.InvitationDTO;
using Application.FiltersExtensions.Invitations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InvitationController : BaseApiController
    {
        public InvitationController(IMediator mediator) : base(mediator)
        { 
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetInvitations()
        {
            var result = await _mediator.Send(new InvitationsList.Query());
            return HandleResult(result);
        }

        [HttpGet("allForPatient/{patientId}")]
        public async Task<IActionResult> GetInvitationsForPatient(int patientId)
        {
            var result = await _mediator.Send(new InvitationsPatientList.Query { PatientId = patientId } );
            return HandleResult(result);
        }

        [HttpGet("allForDietician/{dieticianId}")]
        public async Task<IActionResult> GetInvitationsForDietician([FromQuery] InvitationParams pagingParams, int dieticianId)
        {
            var result = await _mediator.Send(new InvitationsDieticianList.Query { Params = pagingParams, DieticianId = dieticianId });
            return HandlePagedResult(result);
        }

        [HttpGet("details/{invitationId}")]
        public async Task<IActionResult> GetInvitation(int invitationId)
        {
            var result = await _mediator.Send(new InvitationDetails.Query { InvitationId = invitationId });
            return HandleResult(result);
        }

        [HttpPost("send")]
        public async Task<IActionResult> InvitationMessageToDietetician(InvitationPostDTO invitationPostDto)
        {
            var command = new InvitationCreate.Command
            {
                InvitationPostDTO = invitationPostDto
            };
            return HandleResult(await _mediator.Send(command));
        }

        [HttpPut("confirm/{invitationId}")]
        public async Task<IActionResult> AcceptInvitation(int invitationId, InvitationPutDTO invitationPutDTO)
        {
            var command = new InvitationAcceptEdit.Command
            {
                InvitationId = invitationId,
                InvitationPutDTO = invitationPutDTO,
            };
            return HandleResult(await _mediator.Send(command));
        }

        [HttpPut("decline/{invitationId}")]
        public async Task<IActionResult> DeclineInvitation(int invitationId, InvitationPutDTO invitationPutDTO)
        {
            var command = new InvitationDeclineEdit.Command
            {
                InvitationId = invitationId,
                InvitationPutDTO = invitationPutDTO,
            };
            return HandleResult(await _mediator.Send(command));
        }

        [HttpPut("resend/{invitationId}")]
        public async Task<IActionResult> ResendInvitation(int invitationId, InvitationPutDTO invitationPutDTO)
        {
            var command = new InvitationResendEdit.Command
            {
                InvitationId = invitationId,
                InvitationPutDTO = invitationPutDTO,
            };
            return HandleResult(await _mediator.Send(command));
        }
    }
}