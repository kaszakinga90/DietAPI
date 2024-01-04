﻿using Application.Core;
using Application.CQRS.Admins;
using Application.DTOs.AdminDTO;
using Application.Services;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly ImageService _imageService;
        private readonly DietContext _context;

        public AdminController(ImageService imageService, DietContext context, IMediator mediator) : base(mediator)
        {
            _imageService = imageService;
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Admin>>> GetAdmins()
        {
            return await _mediator.Send(new AdminList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminGetDTO>> GetAdmin(int id)
        {
            return await _mediator.Send(new AdminDetails.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(AdminPostDTO AdminPostDTO)
        {
            var result = await _mediator.Send(new AdminCreate.Command { AdminPostDTO = AdminPostDTO });
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdmin(int id, [FromForm] AdminDTO adminDTO, [FromForm] IFormFile file)
        {
            var command = new AdminEdit.Command
            {
                Admin = adminDTO,
                File = file
            };
            command.Admin.Id = id;

            return HandleResult(await _mediator.Send(command));
        }

        [HttpGet("{adminId}/messages")]
        public async Task<ActionResult<PagedList<MessageToDTO>>> GetMessagesForAdmin(int adminId, [FromQuery] PagingParams param)
        {
            var result = await _mediator.Send(new AdminMessageList.Query 
            { 
                AdminId = adminId, 
                Params = param 
            });

            return HandlePagedResult(result);
        }

        /// <summary>
        /// Wysyła wiadomość do dietetyka.
        /// </summary>
        /// <param name="message">Wiadomość dla dietetyka.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("{adminId}/messageToDietician")]
        public async Task<IActionResult> MessageToDietetician(int adminId, MessageToDTO message)
        {
            var command = new MessageToDieteticianFromAdminCreate.Command 
            { 
                MessageDTO = message, 
                AdminId = adminId 
            };

            return HandleResult(await _mediator.Send(command));
        }

        /// <summary>
        /// Wysyła wiadomość do pacjenta.
        /// </summary>
        /// <param name="message">Wiadomość dla pacjenta.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost("{adminId}/messageToPatient")]
        public async Task<IActionResult> MessageToPatient(int adminId, MessageToDTO message)
        {
            var command = new MessageToPatientFromAdminCreate.Command
            {
                MessageDTO = message,
                AdminId = adminId
            };

            return HandleResult(await _mediator.Send(command));
        }
    }
}
